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

        [Fact]
        public void DeveInformarNotaDoAlunoNaMatricula()
        {
            const double notaEsperada = 8.9;
            var matricula = MatriculaBuilder.Novo().Construir();
            matricula.InformarNota(notaEsperada);
            Assert.Equal(notaEsperada, matricula.NotaAluno);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NaoDeveInformarNotaDoAlunoInvalida(double notaAlunoInvalida)
        {
            var matricula = MatriculaBuilder.Novo().Construir();

            Assert.Throws<DomainException>(() =>
                matricula.InformarNota(notaAlunoInvalida)
            ).ComMensagem(Resources.NotaAlunoInvalida);
        }

        [Fact]
        public void DeveInformarQueCursoFoiConcluido()
        {
            const double notaEsperada = 8.9;
            var matricula = MatriculaBuilder.Novo().Construir();
            matricula.InformarNota(notaEsperada);
            Assert.True(matricula.CursoConcluido);
        }


        [Fact]
        public void DeveCancelarMatricula()
        {            
            var matricula = MatriculaBuilder.Novo().Construir();
            
            matricula.Cancelar();

            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaEstiverCancelada()
        {
            const double nota = 8.9;
            var matricula = MatriculaBuilder.Novo().ComCancelada(true).Construir();
            
            Assert.Throws<DomainException>(() =>
                matricula.InformarNota(nota)
            ).ComMensagem(Resources.MatriculaCancelada);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaEstiverConcluido()
        {
            var matricula = MatriculaBuilder.Novo().ComConcluido(true).Construir();

            Assert.Throws<DomainException>(() =>
                matricula.Cancelar()
            ).ComMensagem(Resources.MatriculaConcluida);
        }
    }    
}