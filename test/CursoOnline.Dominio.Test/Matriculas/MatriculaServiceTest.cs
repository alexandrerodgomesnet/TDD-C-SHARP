using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Services;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.UseCases;
using CursoOnline.Dominio.Utils;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
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

            _aluno = AlunoBuilder.Novo().ComId(1).Construir();
            _mockAluno.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComId(1).Construir();
            _mockCurso.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(_curso);

            _matriculaDTO = new MatriculaDTO { AlunoId = _aluno.Id, CursoId = _curso.Id, Valor = _curso.Valor };

            _service = new MatriculaService(_mockCurso.Object, _mockAluno.Object, _mockMatricula.Object) ;
        }

        [Fact]
        public void DeveCursoSerValido()
        {
            Curso cursoInvalido = null;
            _mockCurso.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(cursoInvalido);

            Assert.Throws<DomainException>(() => _service.Criar(_matriculaDTO))
                .ComMensagem(Resources.CursoNaoEncontrado);
        }


        [Fact]
        public void DeveAlunoSerValido()
        {
            Aluno alunoInvalido = null;
            _mockAluno.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(alunoInvalido);

            Assert.Throws<DomainException>(() => _service.Criar(_matriculaDTO))
                .ComMensagem(Resources.AlunoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _service.Criar(_matriculaDTO);

            _mockMatricula.Verify(r => r.Inserir(It.Is<Matricula>(x => x.Aluno == _aluno && x.Curso == _curso)));
        }
    }
}
