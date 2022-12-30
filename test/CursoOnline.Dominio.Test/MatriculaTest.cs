using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Utils.Test.Builders;
using CursoOnline.Utils.Test;
using CursoOnline.Dominio.Utils;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            var curso = CursoBuilder.Novo().Construir();
            var aluno = AlunoBuilder.Novo().Construir();
            var valorMatricula = curso.Valor;

            var matriculaEsperada = new { 
                Curso = curso,
                Aluno = aluno,
                Valor = valorMatricula
            };

            var matricula = MatriculaBuilder
                .Novo()
                .ComCurso(curso)
                .ComAluno(aluno)
                .ComValor(valorMatricula)
                .Construir();

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void QuandoMatriculaCriadaTerAlunoInvalidoEntaoEstouraExcessao()
        {
            Assert.Throws<DomainException>(() => MatriculaBuilder.Novo().ComAluno(null).Construir())
                .ComMensagem(Resources.AlunoInvalido);
        }

        [Fact]
        public void QuandoMatriculaCriadaTerCursoInvalidoEntaoEstouraExcessao()
        {
            Assert.Throws<DomainException>(() => MatriculaBuilder.Novo().ComCurso(null).Construir())
                .ComMensagem(Resources.CursoInvalido);
        }

        [Fact]
        public void QuandoMatriculaCriadaTerValorInvalidoEntaoEstouraExcessao()
        {
            Assert.Throws<DomainException>(() => MatriculaBuilder.Novo().ComValor(0).Construir())
                .ComMensagem(Resources.ValorMatriculaInvalido);
        }

        [Fact]
        public void QuandoMatriculaCriadaTerValorMaiorQueValorDoCursoEntaoEstouraExcessao()
        {
            var curso = CursoBuilder.Novo().ComValor(260M).Construir();
            var valorPagoNaMatricula = curso.Valor + 1;

            Assert.Throws<DomainException>(() => 
                MatriculaBuilder
                    .Novo()
                    .ComCurso(curso)
                    .ComValor(valorPagoNaMatricula)
                    .Construir()
            ).ComMensagem(Resources.ValorDaMatriculaMaiorQueValorDoCUrso);
        }

        [Fact]
        public void DeveCriarMatriculaComDesconto()
        {
            var curso = CursoBuilder.Novo().ComValor(260M).Construir();
            var valorPagoNaMatricula = curso.Valor - 1;

            var matricula = MatriculaBuilder
                .Novo()
                .ComCurso(curso)
                .ComValor(valorPagoNaMatricula)
                .Construir();

            Assert.True(matricula.TemDesconto);
        }

        [Fact]
        public void DevePublicoAlvoSeremIguais()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Construir();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Universitario).Construir();

            Assert.Throws<DomainException>(() => 
                MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Construir()
            ).ComMensagem(Resources.PublicoAlvoDiferentes);
        }
    }    
}
