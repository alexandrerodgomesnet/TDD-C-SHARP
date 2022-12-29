﻿using CursoOnline.Dominio.Data.Contracts;
using CursoOnline.Dominio.UseCases;

namespace CursoOnline.Dominio.Contracts
{
    public interface ICursoRepositorio : IRepositorioBase<Curso>
    {
        Curso ObterCursoPeloNome(string nome);
    }
}