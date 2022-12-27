using CursoOnline.Dominio.Test.Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest
	{
		[Fact]
		public void DeveCriarCurso()
		{
			var cursoEsperado = new
			{
				Nome = "Excel Avançado",
				CargaHoraria = 80.00M,
				PublicoAlvo = PublicoAlvo.Estudantes,
				Valor = 620M
			};

			var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NomeDoCursoNaoPodeSerInvalido(string nome)
		{
			var cursoEsperado = new
			{
				Nome = "Excel Avançado",
				CargaHoraria = 80.00M,
				PublicoAlvo = PublicoAlvo.Estudantes,
				Valor = 620M
			};

			Assert.Throws<ArgumentException>(() => 
				new Curso(nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
				.ComMensagem(Resources.NomeDoCursoInvalido);
		}

		[Fact]
		public void CargaHorariaDeveSerMaiorQueZero()
		{
			var cursoEsperado = new
			{
				Nome = "Excel Avançado",
				CargaHoraria = 80.00M,
				PublicoAlvo = PublicoAlvo.Estudantes,
				Valor = 620M
			};

			Assert.Throws<ArgumentException>(() =>
				new Curso(cursoEsperado.Nome, 0, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
				.ComMensagem(Resources.CargaHorariaDoCursoInvalida);
		}

		[Fact]
		public void ValorDeveSerMaiorQueZero()
		{
			var cursoEsperado = new
			{
				Nome = "Excel Avançado",
				CargaHoraria = 80.00M,
				PublicoAlvo = PublicoAlvo.Estudantes,
				Valor = 620M
			};

			Assert.Throws<ArgumentException>(() =>
				new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, 0))
				.ComMensagem(Resources.ValorDoCursoInvalido);
		}
	}

	public static class Resources
	{
		public static string NomeDoCursoInvalido = "Nome do curso não pode ser inválido!";
		public static string CargaHorariaDoCursoInvalida = "Carga Horária do curso não pode ser inválida!";
		public static string ValorDoCursoInvalido = "Valor do curso não pode ser inválido!";
	}

	public enum PublicoAlvo
	{
		Estudantes,
		Universitario,
		Empregado,
		Empreendedor
	}

	public class Curso
	{
		public Curso(string nome, decimal cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
		{
			if (string.IsNullOrEmpty(nome))
				throw new ArgumentException(Resources.NomeDoCursoInvalido);

			if(cargaHoraria <= 0)
				throw new ArgumentException(Resources.CargaHorariaDoCursoInvalida);

			if (valor <= 0)
				throw new ArgumentException(Resources.ValorDoCursoInvalido);

			Nome = nome;
			CargaHoraria = cargaHoraria;
			PublicoAlvo = publicoAlvo;
			Valor = valor;
		}

		public string Nome { get; private set; }
		public decimal CargaHoraria { get; private set; }
		public PublicoAlvo PublicoAlvo { get; private set; }
		public decimal Valor { get; private set; }
	}
}
