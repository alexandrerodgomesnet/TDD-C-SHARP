using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.UseCases;
using CursoOnline.Dominio.Utils;
using System;

namespace CursoOnline.Dominio.Services
{
    public class AlunoService
    {
        private readonly IAlunoRepositorio _repo;

        public AlunoService(IAlunoRepositorio repo)
        {
            _repo = repo;
        }

        public void Adicionar(AlunoDTO alunoDTO)
        {
            var alunoCadastrado = _repo.ObterPorCpf(alunoDTO.Cpf);

            ValidateExceptions
                .New()
                .When(alunoCadastrado != null, Resources.AlunoExistenteNaBase)
                .DisplayExceptions();

            if (alunoDTO.Id == Guid.Empty)
            {
                var publicoAlvo = alunoDTO.PublicoAlvo.ConverterPublicoAlvo();
                var aluno = new Aluno(alunoDTO.Nome, alunoDTO.Email, alunoDTO.Cpf, publicoAlvo);
                _repo.Adicionar(aluno);
            }
            else
            {
                var aluno = _repo.ObterPorId(alunoDTO.Id);
                aluno.AlterarNome(alunoDTO.Nome);
            }
        }
    }
}
