using CursoOnline.Dominio.UseCases;
using System;

namespace CursoOnline.Dominio.Contracts
{
    public interface IAlunoRepositorio : IRepositorioBase<Aluno, Guid>
    {
        Aluno ObterPorCpf(string cpf);
    }
}