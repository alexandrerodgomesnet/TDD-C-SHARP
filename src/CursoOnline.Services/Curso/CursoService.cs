using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Utils;
using System;

namespace CursoOnline.Services
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
                .When(cursoSalvo != null && cursoSalvo.Id != cursoDTO.Id, Resources.NomeCursoExistente)
                .DisplayExceptions();

            var publicoAlvo = cursoDTO.PublicoAlvo.ConverterPublicoAlvo();

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, publicoAlvo, cursoDTO.Valor);

            if (cursoDTO.Id != Guid.Empty)
            {
                curso = _repo.ObterPorId(cursoDTO.Id);
                curso.EditarNome(cursoDTO.Nome);
                curso.EditarCargaHoraria(cursoDTO.CargaHoraria);
                curso.EditarValor(cursoDTO.Valor);
            }
            else
                _repo.Adicionar(curso);
        }
    }
}