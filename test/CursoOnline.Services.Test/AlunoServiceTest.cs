using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Shared;
using CursoOnline.Utils.Test;
using CursoOnline.Dominio.Utils;
using CursoOnline.Utils.Test.Builders;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Services.Test
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

            _mock.Verify(r => r.Adicionar(It.Is<Aluno>(a => a.Nome == _alunoDTO.Nome)));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaCadastrado()
        {
            var alunoCadastrado = AlunoBuilder.Novo().ComId(Guid.NewGuid()).Construir();
            _mock.Setup(r => r.ObterPorCpf(_alunoDTO.Cpf)).Returns(alunoCadastrado);

            Assert.Throws<DomainException>(() => _alunoService.Adicionar(_alunoDTO))
                .ComMensagem(Resources.AlunoExistenteNaBase);
        }

        [Fact]
        public void DeveEditarNomeAluno()
        {
            _alunoDTO.Id = Guid.NewGuid();
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
            _alunoDTO.Id = Guid.NewGuid();
            var alunoExistente = AlunoBuilder.Novo().Construir();
            _mock.Setup(r => r.ObterPorId(_alunoDTO.Id)).Returns(alunoExistente);

            _alunoService.Adicionar(_alunoDTO);

            _mock.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}