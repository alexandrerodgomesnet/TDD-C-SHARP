using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.UseCases;
using System;
using System.Data.Entity;

namespace CursoOnline.Dominio.Data.Repositorios
{
    public class CursoRepositorio : RepositoryBase<Curso, Guid>, ICursoRepositorio
    {
        protected readonly DbContext _context;

        public CursoRepositorio(DbContext context) : base(context)
        {
            _context = context;
        }
        public Curso ObterCursoPeloNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
