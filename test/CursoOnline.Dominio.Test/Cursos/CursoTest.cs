using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.UseCases;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest : IDisposable
	{
		private readonly ITestOutputHelper _output;
		private readonly string _nome;
		private readonly decimal _cargaHoraria;
		private readonly PublicoAlvo _publicoAlvo;
		private readonly decimal _valor;

		public CursoTest(ITestOutputHelper output)
		{
			_output = output;
			_output.WriteLine("Construtor sendo executado");

			_nome = "Excel Avançado";
			_cargaHoraria = 80.00M;
			_publicoAlvo = PublicoAlvo.Estudantes;
			_valor = 580.00M;
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
				CargaHoraria = _cargaHoraria,
				PublicoAlvo = _publicoAlvo,
				Valor = _valor
			};

			var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void NomeDoCursoNaoPodeSerInvalido(string nome)
		{
			Assert.Throws<ArgumentException>(() => 
				new Curso(nome, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem(Resources.NomeDoCursoInvalido);
		}

		[Fact]
		public void CargaHorariaDeveSerMaiorQueZero()
		{
			Assert.Throws<ArgumentException>(() =>
				new Curso(_nome, 0, _publicoAlvo, _valor)).ComMensagem(Resources.CargaHorariaDoCursoInvalida);
		}

		[Fact]
		public void ValorDeveSerMaiorQueZero()
		{
			Assert.Throws<ArgumentException>(() =>
				new Curso(_nome, _cargaHoraria, _publicoAlvo, 0)).ComMensagem(Resources.ValorDoCursoInvalido);
		}
	}
}