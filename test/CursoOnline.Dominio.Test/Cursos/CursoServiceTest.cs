using Bogus;
using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.UseCases;
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
                PublicoAlvo = 1,
                Valor = faker.Random.Decimal(1, 500)
            };

            _mock = new Mock<ICursoRepositorio>();

            _service = new CursoService(_mock.Object);
        }

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

            _service.Adicionar(cursoDTO);

            // mock.Verify((repo) => repo.Inserir(It.IsAny<Curso>()));
            _mock.Verify((repo) => repo.Inserir(
                It.Is<Curso>(c => 
                    c.Nome == cursoDTO.Nome &&
                    c.Descricao == cursoDTO.Descricao &&
                    c.CargaHoraria == cursoDTO.CargaHoraria &&
                    // c.PublicoAlvo == (PublicoAlvo)cursoDTO.PublicoAlvo &&
                    c.Valor == cursoDTO.Valor
                )
            ), Times.Exactly(1));
        }
    }
}