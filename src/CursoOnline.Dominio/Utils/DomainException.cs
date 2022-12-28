using System;
using System.Collections.Generic;

namespace CursoOnline.Dominio.Utils
{
    public class DomainException : ArgumentException
    {
        public List<string> Messages { get; set; }

        public DomainException(List<string> messages)
        {
            Messages = messages;
        }
    }
}