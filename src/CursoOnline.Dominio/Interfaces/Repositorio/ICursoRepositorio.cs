using CursoOnline.Dominio.Interfaces.Repositorio.Base;
using CursoOnline.Dominio.Entities;
using System;

namespace CursoOnline.Dominio.Interfaces.Repositorio
{
    public interface ICursoRepositorio : IRepositorioBase<Curso, Guid>
    {
        Curso ObterCursoPeloNome(string nome);
    }
}