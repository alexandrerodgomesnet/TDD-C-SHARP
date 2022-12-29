using System.Collections.Generic;

namespace CursoOnline.Dominio.Data.Contracts
{
    public interface IRepositorioBase<T>
    {
        T ObterPorId(int id);
        IEnumerable<T> Listar();
        void Inserir(T model);
        void Atualizar(T model);
        void Excluir(int id);
    }
}