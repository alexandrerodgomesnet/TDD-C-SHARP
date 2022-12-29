using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.UseCases;
using System;
using System.Data.Entity;

namespace CursoOnline.Dominio.Data.Repositorios
{
    public class AlunoRepositorio : RepositoryBase<Aluno, Guid>, IAlunoRepositorio
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
