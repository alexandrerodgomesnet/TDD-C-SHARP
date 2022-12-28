using Bogus;
using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.UseCases;
using CursoOnline.Dominio.Utils;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
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
            _service.Adicionar(_cursoDTO);

            // mock.Verify((repo) => repo.Inserir(It.IsAny<Curso>()));
            _mock.Verify((repo) => repo.Inserir(
                It.Is<Curso>(c => 
                    c.Nome == _cursoDTO.Nome &&
                    c.Descricao == _cursoDTO.Descricao &&
                    c.CargaHoraria == _cursoDTO.CargaHoraria &&
                    // c.PublicoAlvo == (PublicoAlvo)cursoDTO.PublicoAlvo &&
                    c.Valor == _cursoDTO.Valor
                )
            ), Times.Exactly(1));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            _cursoDTO.PublicoAlvo = "Policiais";

            Assert.Throws<GenericExceptions<ArgumentException>>(() => _service.Adicionar(_cursoDTO))
                .ComMensagem(Resources.PublicoAlvoInvalido);
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeJaSalvo()
        {
            var cursoExistente = CursoBuilder.Novo().ComNome(_cursoDTO.Nome).Construir();

            _mock.Setup(r => r.ObterCursoPeloNome(_cursoDTO.Nome)).Returns(cursoExistente);

            Assert.Throws<GenericExceptions<ArgumentException>>(() => _service.Adicionar(_cursoDTO))
                .ComMensagem(Resources.NomeCursoExistente);
        }
    }
}