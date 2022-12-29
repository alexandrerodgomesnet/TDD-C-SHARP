using CursoOnline.Dominio.Data.Contracts;
using CursoOnline.Dominio.UseCases;

namespace CursoOnline.Dominio.Contracts
{
    public interface IAlunoRepositorio : IRepositorioBase<Aluno>
    {
        Aluno ObterPorCpf(string cpf);
    }
}