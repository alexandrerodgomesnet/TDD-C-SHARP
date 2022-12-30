using CursoOnline.Dominio.Interfaces.Repositorio.Base;
using CursoOnline.Dominio.Entities;
using System;

namespace CursoOnline.Dominio.Interfaces.Repositorio
{
    public interface IAlunoRepositorio : IRepositorioBase<Aluno, Guid>
    {
        Aluno ObterPorCpf(string cpf);
    }
}