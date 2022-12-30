using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Entities;
using System;
using System.Data.Entity;

namespace CursoOnline.Data.Repositorios
{
    public class MatriculaRepositorio : RepositorioBase<Matricula, Guid>, IMatriculaRepositorio
    {
        protected readonly DbContext _context;

        public MatriculaRepositorio(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}