using Bogus;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Shared;
using CursoOnline.Utils.Test.Builders;
using CursoOnline.Utils.Test;
using Moq;
using System;
using Xunit;
using CursoOnline.Dominio.Utils;

namespace CursoOnline.Services.Test
{
    public class CursoServiceTest
    {
        private readonly CursoDTO _cursoDTO;
        private readonly Mock<ICursoRepositorio> _mock;
        private readonly CursoService _service;

        public CursoServiceTest()
        {
            var faker = new Faker();

            _cursoDTO = new CursoDTO
            {
                Id = Guid.NewGuid(),
                Nome = faker.Random.Words(),
                Descricao = faker.Lorem.Paragraphs(),
                CargaHoraria = faker.Random.Decimal(1, 80),
                PublicoAlvo = "Estudantes",
                Valor = faker.Random.Decimal(1, 500)
            };

            _mock = new Mock<ICursoRepositorio>();

            _service = new CursoService(_mock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _cursoDTO.Id = Guid.Empty;
            _service.Adicionar(_cursoDTO);

            _mock.Verify((repo) => repo.Adicionar(
                It.Is<Curso>(c =>
                    c.Nome == _cursoDTO.Nome &&
                    c.Descricao == _cursoDTO.Descricao &&
                    c.CargaHoraria == _cursoDTO.CargaHoraria &&
                    c.Valor == _cursoDTO.Valor
                )
            ), Times.Exactly(1));
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeJaSalvo()
        {
            var cursoExistente = CursoBuilder.Novo().ComId(Guid.NewGuid()).ComNome(_cursoDTO.Nome).Construir();

            _mock.Setup(r => r.ObterCursoPeloNome(_cursoDTO.Nome)).Returns(cursoExistente);

            Assert.Throws<DomainException>(() => _service.Adicionar(_cursoDTO))
                .ComMensagem(Resources.NomeCursoExistente);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDTO.Id = Guid.NewGuid();
            var curso = CursoBuilder.Novo().Construir();
            _mock.Setup(r => r.ObterPorId(_cursoDTO.Id)).Returns(curso);

            _service.Adicionar(_cursoDTO);

            Assert.Equal(_cursoDTO.Nome, curso.Nome);
            Assert.Equal(_cursoDTO.CargaHoraria, curso.CargaHoraria);
            Assert.Equal(_cursoDTO.Valor, curso.Valor);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExistir()
        {
            _cursoDTO.Id = Guid.NewGuid();
            var curso = CursoBuilder.Novo().Construir();
            _mock.Setup(r => r.ObterPorId(_cursoDTO.Id)).Returns(curso);

            _service.Adicionar(_cursoDTO);

            _mock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);
        }
    }
}
