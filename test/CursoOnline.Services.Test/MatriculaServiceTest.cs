using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Shared;
using CursoOnline.Utils.Test.Builders;
using CursoOnline.Utils.Test;
using CursoOnline.Dominio.Entities;
using CursoOnline.Dominio.Utils;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Services.Test
{
    public class MatriculaServiceTest
    {
        #region Campos privados...
        private readonly Mock<IMatriculaRepositorio> _mockMatricula;
        private readonly Mock<ICursoRepositorio> _mockCurso;
        private readonly Mock<IAlunoRepositorio> _mockAluno;
        private readonly MatriculaDTO _matriculaDTO;
        private readonly MatriculaService _service;
        private readonly Curso _curso;
        private readonly Aluno _aluno;
        #endregion

        #region Construtor...
        public MatriculaServiceTest()
        {
            _mockMatricula = new Mock<IMatriculaRepositorio>();
            _mockCurso = new Mock<ICursoRepositorio>();
            _mockAluno = new Mock<IAlunoRepositorio>();

            _aluno = AlunoBuilder.Novo().ComId(Guid.NewGuid()).Construir();
            _mockAluno.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComId(Guid.NewGuid()).Construir();
            _mockCurso.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(_curso);

            _matriculaDTO = new MatriculaDTO { AlunoId = _aluno.Id, CursoId = _curso.Id, Valor = _curso.Valor };

            _service = new MatriculaService(_mockCurso.Object, _mockAluno.Object, _mockMatricula.Object);
        }
        #endregion        

        #region Metodos...
        [Fact]
        public void DeveCursoSerValido()
        {
            Curso cursoInvalido = null;
            _mockCurso.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(cursoInvalido);

            Assert.Throws<DomainException>(() => _service.Criar(_matriculaDTO))
                .ComMensagem(Resources.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveAlunoSerValido()
        {
            Aluno alunoInvalido = null;
            _mockAluno.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(alunoInvalido);

            Assert.Throws<DomainException>(() => _service.Criar(_matriculaDTO))
                .ComMensagem(Resources.AlunoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _service.Criar(_matriculaDTO);

            _mockMatricula.Verify(r => r.Adicionar(It.Is<Matricula>(x => x.Aluno == _aluno && x.Curso == _curso)));
        }

        [Fact]
        public void DeveInformarNotaDoAluno()
        {
            var notaEsperada = 8.9;
            var matricula = MatriculaBuilder.Novo().Construir();

            _mockMatricula.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _service.Concluir(matricula.Id, notaEsperada);

            Assert.Equal(notaEsperada, matricula.NotaAluno);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            var idMatriculaINvalido = Guid.Empty;
            Matricula matriculaInvalida = null;
            var nota = 8.9;

            _mockMatricula.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(matriculaInvalida);

            Assert.Throws<DomainException>(() => _service.Concluir(idMatriculaINvalido, nota))
                .ComMensagem(Resources.MatriculaInvalida);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Construir();
            _mockMatricula.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _service.Cancelar(matricula.Id);

            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontradaAoCancelar()
        {
            var idMatriculaINvalido = Guid.Empty;
            Matricula matriculaInvalida = null;

            _mockMatricula.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(matriculaInvalida);

            Assert.Throws<DomainException>(() => _service.Cancelar(idMatriculaINvalido))
                .ComMensagem(Resources.MatriculaInvalida);
        }

        #endregion
    }
}