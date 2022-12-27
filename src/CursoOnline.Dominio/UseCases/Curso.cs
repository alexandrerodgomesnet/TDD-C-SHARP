using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using System;

namespace CursoOnline.Dominio.UseCases
{
	public class Curso
	{
		public Curso(string nome, decimal cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
		{
			if (string.IsNullOrEmpty(nome))
				throw new ArgumentException(Resources.NomeDoCursoInvalido);

			if (cargaHoraria <= 0)
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