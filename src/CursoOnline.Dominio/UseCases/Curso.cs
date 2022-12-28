using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Utils;
using System;

namespace CursoOnline.Dominio.UseCases
{
	public class Curso
	{
		public Curso(string nome, string descricao, decimal cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
		{
			if (string.IsNullOrEmpty(nome))
				throw new GenericExceptions<ArgumentException>(Resources.NomeDoCursoInvalido);

			if (cargaHoraria <= 0)
				throw new GenericExceptions<ArgumentException>(Resources.CargaHorariaDoCursoInvalida);

			if (valor <= 0)
				throw new GenericExceptions<ArgumentException>(Resources.ValorDoCursoInvalido);

			Nome = nome;
			Descricao = descricao;
			CargaHoraria = cargaHoraria;
			PublicoAlvo = publicoAlvo;
			Valor = valor;
		}

		public string Nome { get; private set; }
		public string Descricao { get; private set; }
		public decimal CargaHoraria { get; private set; }
		public PublicoAlvo PublicoAlvo { get; private set; }
		public decimal Valor { get; private set; }
	}
}