using CursoOnline.Dominio.Data.Contracts;
using System;
using System.Collections.Generic;

namespace CursoOnline.Dominio.Data.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T>
    {
        public void Atualizar(T model)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public void Inserir(T model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Listar()
        {
            throw new NotImplementedException();
        }

        public T ObterPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}