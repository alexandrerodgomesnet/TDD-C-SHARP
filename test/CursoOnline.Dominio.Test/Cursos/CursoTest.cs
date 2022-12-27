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

		[Fact]
		public void NomeDoCursoNaoPodeSerVazio()
		{
			var cursoEsperado = new
			{
				Nome = "Excel Avançado",
				CargaHoraria = 80.00M,
				PublicoAlvo = PublicoAlvo.Estudantes,
				Valor = 620M
			};

			Assert.Throws<ArgumentException>(() => 
				new Curso(string.Empty, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));
		}

		[Fact]
		public void NomeDoCursoNaoPodeSerNulo()
		{
			var cursoEsperado = new
			{
				Nome = "Excel Avançado",
				CargaHoraria = 80.00M,
				PublicoAlvo = PublicoAlvo.Estudantes,
				Valor = 620M
			};

			Assert.Throws<ArgumentException>(() =>
				new Curso(null, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor));
		}
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
				throw new ArgumentException();

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
