using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.UseCases;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoServiceTest
    {
        [Fact]
        public void DeveAdicionarCurso() 
        {
            var cursoDTO = new CursoDTO
            {
                Nome = "Curso",
                Descricao = "Descrição",
                CargaHoraria = 80.00M,
                PublicoAlvo = 1,
                Valor = 520.00M
            };
            var mock = new Mock<ICursoRepositorio>();

            var service = new CursoService(mock.Object);
            service.Adicionar(cursoDTO);

            mock.Verify((repo) => repo.Inserir(It.IsAny<Curso>()));
        }
    }

    public interface ICursoRepositorio
    {
        void Inserir(Curso curso);
    }

    public class CursoService
    {
        private ICursoRepositorio _repo;
        public CursoService(ICursoRepositorio repo)
        {
            _repo = repo;
        }

        public void Adicionar(CursoDTO cursoDTO)
        {
            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, PublicoAlvo.Estudantes, cursoDTO.Valor);

            _repo.Inserir(curso);
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}