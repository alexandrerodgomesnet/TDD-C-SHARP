using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.UseCases;
using CursoOnline.Dominio.Utils;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    public class AlunoServiceTest
    {
        private readonly Faker _faker;
        private readonly AlunoDTO _alunoDTO;
        private readonly AlunoService _alunoService;
        private readonly Mock<IAlunoRepositorio> _mock;

        public AlunoServiceTest()
        {
            _faker = new Faker();
            _alunoDTO = new AlunoDTO
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = "Empreendedor"
            };

            _mock = new Mock<IAlunoRepositorio>();
            _alunoService = new AlunoService(_mock.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _alunoService.Adicionar(_alunoDTO);

            _mock.Verify(r => r.Inserir(It.Is<Aluno>(a => a.Nome == _alunoDTO.Nome)));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaCadastrado()
        {
            var alunoCadastrado = AlunoBuilder.Novo().ComId(1).Construir();
            _mock.Setup(r => r.ObterPorCpf(_alunoDTO.Cpf)).Returns(alunoCadastrado);

            Assert.Throws<DomainException>(() => _alunoService.Adicionar(_alunoDTO))
                .ComMensagem(Resources.AlunoExistenteNaBase);
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "publicoAlvoInvalido";
            _alunoDTO.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<DomainException>(() => _alunoService.Adicionar(_alunoDTO))
                .ComMensagem(Resources.PublicoAlvoInvalido);
        }

        [Fact]
        public void DeveEditarNomeAluno()
        {
            _alunoDTO.Id = 10;
            _alunoDTO.Nome = _faker.Person.FullName;
            var alunoExistente = AlunoBuilder.Novo().Construir();
            _mock.Setup(r => r.ObterPorId(_alunoDTO.Id)).Returns(alunoExistente);

            _alunoService.Adicionar(_alunoDTO);

            Assert.Equal(_alunoDTO.Nome, alunoExistente.Nome);
        }

        [Fact]
        public void NaoDeveEditarOutrasInformacoesDoAluno()
        {
            var alunoExistente = AlunoBuilder.Novo().Construir();
            var cpfEsperado = alunoExistente.Cpf;

            _alunoDTO.Cpf = _faker.Person.Cpf();
            
            _mock.Setup(r => r.ObterPorId(_alunoDTO.Id)).Returns(alunoExistente);

            _alunoService.Adicionar(_alunoDTO);

            Assert.Equal(cpfEsperado, alunoExistente.Cpf);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoForEdicao()
        {
            _alunoDTO.Id = 10;
            var alunoExistente = AlunoBuilder.Novo().Construir();
            _mock.Setup(r => r.ObterPorId(_alunoDTO.Id)).Returns(alunoExistente);

            _alunoService.Adicionar(_alunoDTO);

            _mock.Verify(r => r.Inserir(It.IsAny<Aluno>()), Times.Never);
        }
    }
}