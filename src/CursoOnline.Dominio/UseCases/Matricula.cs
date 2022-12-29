using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Utils;

namespace CursoOnline.Dominio.UseCases
{
    public class Matricula
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal Valor { get; private set; }
        public bool TemDesconto { get; private set; }

        public Matricula(Curso curso, Aluno aluno, decimal valor)
        {
            ValidateExceptions
                .New()                
                .When(curso == null, Resources.CursoInvalido)
                .When(aluno == null, Resources.AlunoInvalido)
                .When(valor <= 0, Resources.ValorMatriculaInvalido)
                .When(curso != null && valor > curso.Valor, Resources.ValorDaMatriculaMaiorQueValorDoCUrso)
                .DisplayExceptions();
            
            Curso = curso;
            Aluno = aluno;
            Valor = valor;
            TemDesconto = valor < curso.Valor;
        }
    }
}