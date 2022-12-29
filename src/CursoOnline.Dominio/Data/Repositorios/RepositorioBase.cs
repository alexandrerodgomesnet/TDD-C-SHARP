using CursoOnline.Dominio.Contracts;
using CursoOnline.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CursoOnline.Dominio.Data.Repositorios
{
    public class RepositoryBase<T, TId> : IRepositorioBase<T, TId>
        where T : EntityBase
        where TId : struct
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> ListarPor(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return Listar(includeProperties).Where(where);
        }

        public IQueryable<T> ListarEOrdenadosPor<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties)
        {
            return ascendente ? ListarPor(where, includeProperties).OrderBy(ordem) : ListarPor(where, includeProperties).OrderByDescending(ordem);
        }

        public T ObterPor(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return Listar(includeProperties).FirstOrDefault(where);
        }

        public T ObterPorId(TId id, params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties.Any())
            {
                return Listar(includeProperties).FirstOrDefault(x => x.Id.ToString() == id.ToString());
            }

            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Listar(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includeProperties.Any())
            {
                return Include(_context.Set<T>(), includeProperties);
            }

            return query;
        }

        public IQueryable<T> ListarOrdenadosPor<TKey>(Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties)
        {
            return ascendente ? Listar(includeProperties).OrderBy(ordem) : Listar(includeProperties).OrderByDescending(ordem);
        }

        public T Adicionar(T entidade)
        {
            return _context.Set<T>().Add(entidade);
        }

        public T Editar(T entidade)
        {
            _context.Entry(entidade).State = System.Data.Entity.EntityState.Modified;

            return entidade;
        }

        public void Remover(T entidade)
        {
            _context.Set<T>().Remove(entidade);
        }

        /// <summary>
        /// Adicionar um coleção de entidades ao contexto do entity framework
        /// </summary>
        /// <param name="entidades">Lista de entidades que deverão ser persistidas</param>
        /// <returns></returns>
        public IEnumerable<T> AdicionarLista(IEnumerable<T> entidades)
        {
            return _context.Set<T>().AddRange(entidades);
        }

        /// <summary>
        /// Verifica se existe algum objeto com a condição informada
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Existe(Func<T, bool> where)
        {
            return _context.Set<T>().Any(where);
        }

        /// <summary>
        /// Realiza include populando o objeto passado por parametro
        /// </summary>
        /// <param name="query">Informe o objeto do tipo IQuerable</param>
        /// <param name="includeProperties">Ínforme o array de expressões que deseja incluir</param>
        /// <returns></returns>
        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            return query;
        }
    }
}