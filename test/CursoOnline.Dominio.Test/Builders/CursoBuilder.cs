using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.UseCases;

namespace CursoOnline.Dominio.Test.Builders
{
	public class CursoBuilder
	{
        #region Campos da classe...
        private string _nome = "Excel Avançado";
		private string _descricao = "Curso de Excel nível Básico.";
		private decimal _cargaHoraria = 80.00M;
		private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudantes;
		private decimal _valor = 580.00M;
        #endregion

        public static CursoBuilder Novo()
		{
			return new CursoBuilder();
		}

		public CursoBuilder ComNome(string nome)
		{
			_nome = nome;
			return this;
		}

		public CursoBuilder ComDescricao(string descricao)
		{
			_descricao = descricao;
			return this;
		}

		public CursoBuilder ComCargaHoraria(decimal cargaHoraria)
		{
			_cargaHoraria = cargaHoraria;
			return this;
		}
		
		public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
		{
			_publicoAlvo = publicoAlvo;
			return this;
		}

		public CursoBuilder ComValor(decimal valor)
		{
			_valor = valor;
			return this;
		}

		public Curso Construir()
		{
			return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
		}

	}
}