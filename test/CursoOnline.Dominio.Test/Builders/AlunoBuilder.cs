using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.UseCases;
using System;

namespace CursoOnline.Dominio.Test.Builders
{
	public class AlunoBuilder
	{
		#region Campos da classe...
		private Guid _id = Guid.NewGuid();
        private string _nome = "José";
		private string _email = "jose@email.com";
		private string _cpf = "123.456.789-00";
		private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudantes;
        #endregion

        public static AlunoBuilder Novo()
		{
			return new AlunoBuilder();
		}

		public AlunoBuilder ComId(Guid id)
		{
			_id = id;
			return this;
		}

		public AlunoBuilder ComNome(string nome)
		{
			_nome = nome;
			return this;
		}

		public AlunoBuilder ComEmail(string email)
		{
			_email = email;
			return this;
		}

		public AlunoBuilder ComCPf(string cpf)
		{
			_cpf = cpf;
			return this;
		}

        public AlunoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

		public Aluno Construir()
		{
			var aluno = new Aluno(_nome, _email, _cpf, _publicoAlvo);

			if (_id == Guid.Empty)
			{
				var properties = aluno.GetType().GetProperty("Id");
				properties.SetValue(aluno, Convert.ChangeType(_id, properties.PropertyType), null);
			}

			return aluno;
		}
	}
}