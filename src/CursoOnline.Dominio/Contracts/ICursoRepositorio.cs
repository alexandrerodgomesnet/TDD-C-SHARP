using CursoOnline.Dominio.UseCases;
using System;

namespace CursoOnline.Dominio.Contracts
{
    public interface ICursoRepositorio : IRepositorioBase<Curso, Guid>
    {
        Curso ObterCursoPeloNome(string nome);
    }
}