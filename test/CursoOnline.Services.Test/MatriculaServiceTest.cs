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
        Mock<IMatriculaRepositorio> _mockMatricula;
        Mock<ICursoRepositorio> _mockCurso;
        Mock<IAlunoRepositorio> _mockAluno;
        MatriculaDTO _matriculaDTO;
        MatriculaService _service;
        private readonly Curso _curso;
        private readonly Aluno _aluno;

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

            _service = new MatriculaService(_mockCurso.Object, _mockAluno.Object, _mockMatricula.Object) ;
        }

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
    }
}
