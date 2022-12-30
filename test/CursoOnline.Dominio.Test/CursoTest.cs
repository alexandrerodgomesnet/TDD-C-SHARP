using Bogus;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Utils.Test.Builders;
using CursoOnline.Utils.Test;
using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Utils;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test
{
	public class CursoTest : IDisposable
	{
		private readonly ITestOutputHelper _output;
		private readonly string _nome;
		private readonly string _descricao;
		private readonly decimal _cargaHoraria;
		private readonly PublicoAlvo _publicoAlvo;
		private readonly decimal _valor;

		public CursoTest(ITestOutputHelper output)
		{
			var faker = new Faker();

			_output = output;
			_output.WriteLine("Construtor sendo executado");

			_nome = faker.Random.Words();
			_descricao = faker.Lorem.Paragraphs();
			_cargaHoraria = faker.Random.Decimal(1, 80);
			_publicoAlvo = PublicoAlvo.Estudantes;
			_valor = faker.Random.Decimal(1, 500);
		}

		public void Dispose() 
		{
			_output.WriteLine("Dispose sendo executado");
		}

		[Fact]
		public void DeveCriarCurso()
		{
			var cursoEsperado = new
			{
				Nome = _nome,
				Descricao = _descricao,
				CargaHoraria = _cargaHoraria,
				PublicoAlvo = _publicoAlvo,
				Valor = _valor
			};

			var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, 
					cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NomeDoCursoNaoPodeSerInvalido(string nome)
		{
			Assert.Throws<DomainException>(() => CursoBuilder.Novo().ComNome(nome).Construir())
				.ComMensagem(Resources.NomeDoCursoInvalido);
		}

		[Fact]
		public void CargaHorariaDeveSerMaiorQueZero()
		{
			Assert.Throws<DomainException>(() => CursoBuilder.Novo().ComCargaHoraria(0).Construir())
				.ComMensagem(Resources.CargaHorariaDoCursoInvalida);
		}

		[Fact]
		public void ValorDeveSerMaiorQueZero()
		{
			Assert.Throws<DomainException>(() => CursoBuilder.Novo().ComValor(0).Construir())
				.ComMensagem(Resources.ValorDoCursoInvalido);
		}

		[Fact]
		public void DeveAlterarNome()
        {
			var nomeEsperado = "Informatica";
			var curso = CursoBuilder.Novo().ComNome(nomeEsperado).Construir();

			curso.EditarNome(nomeEsperado);

			Assert.Equal(nomeEsperado, curso.Nome);
        }

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NomeDoCursoAlteradoNaoPodeSerInvalido(string nome)
		{
			var curso = CursoBuilder.Novo().Construir();

			Assert.Throws<DomainException>(() => curso.EditarNome(nome))
				.ComMensagem(Resources.NomeDoCursoInvalido);
		}

		[Fact]
		public void DeveAlterarCargaHoraria()
		{
			var cargaHorariaEsperada = 50.00M;
			var curso = CursoBuilder.Novo().ComCargaHoraria(cargaHorariaEsperada).Construir();

			curso.EditarCargaHoraria(cargaHorariaEsperada);

			Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
		}

		[Fact]
		public void CargaHorariaAlteradaDeveSerMaiorQueZero()
		{
			var curso = CursoBuilder.Novo().Construir();

			Assert.Throws<DomainException>(() => curso.EditarCargaHoraria(0))
				.ComMensagem(Resources.CargaHorariaDoCursoInvalida);
		}

		[Fact]
		public void DeveAlterarValor()
		{
			var valorEsperada = 150.00M;
			var curso = CursoBuilder.Novo().ComCargaHoraria(valorEsperada).Construir();

			curso.EditarValor(valorEsperada);

			Assert.Equal(valorEsperada, curso.Valor);
		}

        [Fact]
        public void ValorAlteradoDeveSerMaiorQueZero()
        {
            var curso = CursoBuilder.Novo().Construir();

            Assert.Throws<DomainException>(() => curso.EditarValor(0))
                .ComMensagem(Resources.ValorDoCursoInvalido);
        }
    }
}