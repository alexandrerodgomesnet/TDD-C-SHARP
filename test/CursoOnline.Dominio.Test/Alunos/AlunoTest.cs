using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.UseCases;
using CursoOnline.Dominio.Utils;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    public class AlunoTest
    {
        private readonly Faker _faker;
        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empreendedor
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;

            var aluno = AlunoBuilder.Novo().Construir();

            aluno.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, aluno.Nome);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarAlunoComNomeINvalido(string nome)
        {
            Assert.Throws<DomainException>(() =>
                AlunoBuilder.Novo().ComNome(nome).Construir())
                .ComMensagem(Resources.NomeAlunoInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveCriarAlunoComEmailINvalido(string email)
        {
            Assert.Throws<DomainException>(() =>
                AlunoBuilder.Novo().ComEmail(email).Construir())
                .ComMensagem(Resources.EmailInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("cpf invalido")]
        [InlineData("00000000000")]
        public void NaoDeveCriarAlunoComCpfINvalido(string cpf)
        {
            Assert.Throws<DomainException>(() =>
                AlunoBuilder.Novo().ComCPf(cpf).Construir())
                .ComMensagem(Resources.CpfInvalido);
        }
    }    
}
