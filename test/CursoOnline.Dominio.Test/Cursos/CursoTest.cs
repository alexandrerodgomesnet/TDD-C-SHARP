using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.UseCases;
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
}