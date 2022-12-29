using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Utils;

namespace CursoOnline.Dominio.UseCases
{
    public class Matricula : EntityBase
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal Valor { get; private set; }
        public bool TemDesconto { get; private set; }

        public Matricula(Curso curso, Aluno aluno, decimal valor)
        {
            bool cursoInvalido = curso == null;
            bool alunoInvalido = aluno == null;
            bool valorInvalido = valor <= 0;
            bool valorDaMatriculaMaior = !cursoInvalido && valor > curso.Valor;
            bool publicoAlvoDiferente = !cursoInvalido && !alunoInvalido && aluno.PublicoAlvo != curso.PublicoAlvo;

            ValidateExceptions
                .New()                
                .When(cursoInvalido, Resources.CursoInvalido)
                .When(alunoInvalido, Resources.AlunoInvalido)
                .When(valorInvalido, Resources.ValorMatriculaInvalido)
                .When(valorDaMatriculaMaior, Resources.ValorDaMatriculaMaiorQueValorDoCUrso)
                .When(publicoAlvoDiferente, Resources.PublicoAlvoDiferentes)
                .DisplayExceptions();
            
            Curso = curso;
            Aluno = aluno;
            Valor = valor;
            TemDesconto = valor < curso.Valor;
        }
    }
}