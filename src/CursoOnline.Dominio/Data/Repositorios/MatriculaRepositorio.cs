using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.UseCases;
using System;
using System.Data.Entity;

namespace CursoOnline.Dominio.Data.Repositorios
{
    public class MatriculaRepositorio : RepositoryBase<Matricula, Guid>, IMatriculaRepositorio
    {
        protected readonly DbContext _context;

        public MatriculaRepositorio(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}