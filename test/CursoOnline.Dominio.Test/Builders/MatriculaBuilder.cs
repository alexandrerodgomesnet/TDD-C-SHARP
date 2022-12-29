using CursoOnline.Dominio.UseCases;

namespace CursoOnline.Dominio.Test.Builders
{
    public class MatriculaBuilder
	{
        #region Campos da classe...
        private Aluno _aluno;
		private Curso _curso;
		private decimal _valor;
        #endregion

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

		public Matricula Construir()
		{
			return new Matricula(_curso, _aluno, _valor);
		}
	}
}