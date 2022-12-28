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

            if (cursoSalvo != null)
                throw new GenericExceptions<ArgumentException>(Resources.NomeCursoExistente);

            if (!Enum.TryParse<PublicoAlvo>(cursoDTO.PublicoAlvo, out var publicoAlvo))
                throw new GenericExceptions<ArgumentException>(Resources.PublicoAlvoInvalido);

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, publicoAlvo, cursoDTO.Valor);

            _repo.Inserir(curso);
        }
    }
}