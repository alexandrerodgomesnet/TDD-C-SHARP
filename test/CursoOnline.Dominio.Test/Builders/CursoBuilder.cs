using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.UseCases;
using System;

namespace CursoOnline.Dominio.Test.Builders
{
	public class CursoBuilder
	{
		#region Campos da classe...
		private Guid _id = Guid.NewGuid();
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

		public CursoBuilder ComId(Guid id)
		{
			_id = id;
			return this;
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
			var curso = new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
			if(_id == Guid.Empty)
            {
				var properties = curso.GetType().GetProperty("Id");
				properties.SetValue(curso, Convert.ChangeType(_id, properties.PropertyType), null);
            }
			return curso;
		}
	}
}