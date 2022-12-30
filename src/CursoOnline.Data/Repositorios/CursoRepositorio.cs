using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Entities;
using System;
using System.Data.Entity;

namespace CursoOnline.Data.Repositorios
{
    public class CursoRepositorio : RepositorioBase<Curso, Guid>, ICursoRepositorio
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