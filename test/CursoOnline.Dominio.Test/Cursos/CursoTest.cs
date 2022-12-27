using ExpectedObjects;
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
				PublicoAlvo = "Estudantes",
				Valor = 620M
			};

			var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

			cursoEsperado.ToExpectedObject().ShouldMatch(curso);
		}		
	}

	public class Curso
	{
		public Curso(string nome, decimal cargaHoraria, string publicoAlvo, decimal valor)
		{
			Nome = nome;
			CargaHoraria = cargaHoraria;
			PublicoAlvo = publicoAlvo;
			Valor = valor;
		}

		public string Nome { get; private set; }
		public decimal CargaHoraria { get; private set; }
		public string PublicoAlvo { get; private set; }
		public decimal Valor { get; private set; }
	}
}
