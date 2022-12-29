using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CursoOnline.Dominio.Contracts
{
    public interface IRepositorioBase<T, TId>
       where T : class
       where TId : struct
    {
        IQueryable<T> ListarPor(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> ListarEOrdenadosPor<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties);

        T ObterPor(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties);

        bool Existe(Func<T, bool> where);

        IQueryable<T> Listar(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> ListarOrdenadosPor<TKey>(Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties);

        T ObterPorId(TId id, params Expression<Func<T, object>>[] includeProperties);

        T Adicionar(T entidade);

        T Editar(T entidade);

        void Remover(T entidade);

        IEnumerable<T> AdicionarLista(IEnumerable<T> entidades);
    }
}
