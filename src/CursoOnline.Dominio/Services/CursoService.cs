using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.UseCases;
using CursoOnline.Dominio.Utils;
using System;

namespace CursoOnline.Dominio.Services
{
    public class CursoService
    {
        private ICursoRepositorio _repo;
        public CursoService(ICursoRepositorio repo)
        {
            _repo = repo;
        }

        public void Adicionar(CursoDTO cursoDTO)
        {
            var cursoSalvo = _repo.ObterCursoPeloNome(cursoDTO.Nome);

            ValidateExceptions
                .New()
                .When(cursoSalvo != null, Resources.NomeCursoExistente)
                .When((!Enum.TryParse<PublicoAlvo>(cursoDTO.PublicoAlvo, out var publicoAlvo)), Resources.PublicoAlvoInvalido)
                .DisplayExceptions();

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, publicoAlvo, cursoDTO.Valor);

            if (cursoDTO.Id > 0)
            {
                curso = _repo.ObterPorId(cursoDTO.Id);
                curso.EditarNome(cursoDTO.Nome);
                curso.EditarCargaHoraria(cursoDTO.CargaHoraria);
                curso.EditarValor(cursoDTO.Valor);
            }
            else
                _repo.Inserir(curso);
        }
    }
}