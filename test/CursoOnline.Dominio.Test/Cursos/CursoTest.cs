using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
	public class CursoTest
	{
		[Fact]
		public void DeveCriarCurso()
		{
			string nome = "Excel Avançado";
			decimal cargaHoraria = 80;
			string publicoAlvo = "Estudantes";
			decimal valor = 620;
			var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

			Assert.Equal(nome, curso.Nome);
			Assert.Equal(cargaHoraria, curso.CargaHoraria);
			Assert.Equal(publicoAlvo, curso.PublicoAlvo);
			Assert.Equal(valor, curso.Valor);
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
