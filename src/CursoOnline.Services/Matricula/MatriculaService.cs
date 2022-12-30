using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Utils;
using System;

namespace CursoOnline.Services
{
    public class MatriculaService
    {
        private readonly ICursoRepositorio _cursoRepo;
        private readonly IAlunoRepositorio _alunoRepo;
        private readonly IMatriculaRepositorio _matriculaRepo;

        public MatriculaService(ICursoRepositorio cursoRepo, IAlunoRepositorio alunoRepo, IMatriculaRepositorio matriculaRepo)
        {
            _cursoRepo = cursoRepo;
            _alunoRepo = alunoRepo;
            _matriculaRepo = matriculaRepo;
        }

        public void Criar(MatriculaDTO matriculaDTO)
        {
            var curso = _cursoRepo.ObterPorId(matriculaDTO.CursoId);
            var aluno = _alunoRepo.ObterPorId(matriculaDTO.AlunoId);

            ValidateExceptions
                .New()
                .When(curso == null, Resources.CursoNaoEncontrado)
                .When(aluno == null, Resources.AlunoNaoEncontrado)
                .DisplayExceptions();

            var matricula = new Matricula(curso, aluno, matriculaDTO.Valor);

            _matriculaRepo.Adicionar(matricula);
        }

        public void Concluir(Guid matriculaId, double nota)
        {
            ValidateExceptions
                .New()
                .When(matriculaId == Guid.Empty, Resources.MatriculaInvalida)
                .DisplayExceptions();

            var matricula = _matriculaRepo.ObterPorId(matriculaId);
            matricula.InformarNota(nota);
        }
    }
}