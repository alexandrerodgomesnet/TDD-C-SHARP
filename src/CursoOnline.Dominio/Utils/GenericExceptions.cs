using System;

namespace CursoOnline.Dominio.Utils
{
    public class GenericExceptions<T> : Exception
    {
        public GenericExceptions() : base() { }
        public GenericExceptions(string mensagem) : base(mensagem) { }
    }
}