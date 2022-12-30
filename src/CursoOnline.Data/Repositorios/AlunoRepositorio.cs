using CursoOnline.Dominio.Interfaces.Repositorio;
using CursoOnline.Dominio.Entities;
using System;
using System.Data.Entity;

namespace CursoOnline.Data.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno, Guid>, IAlunoRepositorio
    {
        protected readonly DbContext _context;

        public AlunoRepositorio(DbContext context) : base(context)
        {
            _context = context;
        }
        public Aluno ObterPorCpf(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
