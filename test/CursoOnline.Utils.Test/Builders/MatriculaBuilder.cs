using CursoOnline.Dominio.Entities;

namespace CursoOnline.Utils.Test.Builders
{
    public class MatriculaBuilder
	{
        #region Campos da classe...
        private Aluno _aluno;
		private Curso _curso;
		private decimal _valor;
		private bool _cancelada;
		private bool _concluido;
		#endregion

		#region Metodos...
		public static MatriculaBuilder Novo()
		{
			var curso = CursoBuilder.Novo().Construir();
			var aluno = AlunoBuilder.Novo().Construir();

			return new MatriculaBuilder
			{
				_curso = curso,
				_aluno = aluno,
				_valor = curso.Valor
			};
		}

		public MatriculaBuilder ComCurso(Curso curso)
		{
			_curso = curso;
			return this;
		}

		public MatriculaBuilder ComAluno(Aluno aluno)
		{
			_aluno = aluno;
			return this;
		}

		public MatriculaBuilder ComValor(decimal valor)
		{
			_valor = valor;
			return this;
		}

		public MatriculaBuilder ComCancelada(bool cancelada)
		{
			_cancelada = cancelada;
			return this;
		}

		public MatriculaBuilder ComConcluido(bool concluido)
		{
			_concluido = concluido;
			return this;
		}

		public Matricula Construir()
		{
			var matricula = new Matricula(_curso, _aluno, _valor);

			if (_cancelada)
				matricula.Cancelar();

			if (_concluido)
			{
				var nota = 7;
				matricula.InformarNota(nota);
			}

			return matricula;
		}
		#endregion
	}
}