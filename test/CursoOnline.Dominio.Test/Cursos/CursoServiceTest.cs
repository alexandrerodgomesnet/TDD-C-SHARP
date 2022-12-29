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

            _mock.Verify((repo) => repo.Inserir(
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
            var cursoExistente = CursoBuilder.Novo().ComId(111).ComNome(_cursoDTO.Nome).Construir();

            _mock.Setup(r => r.ObterCursoPeloNome(_cursoDTO.Nome)).Returns(cursoExistente);

            Assert.Throws<DomainException>(() => _service.Adicionar(_cursoDTO))
                .ComMensagem(Resources.NomeCursoExistente);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDTO.Id = 357;
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
            _cursoDTO.Id = 357;
            var curso = CursoBuilder.Novo().Construir();
            _mock.Setup(r => r.ObterPorId(_cursoDTO.Id)).Returns(curso);

            _service.Adicionar(_cursoDTO);

            _mock.Verify(r => r.Inserir(It.IsAny<Curso>()), Times.Never);
        }
    }
}