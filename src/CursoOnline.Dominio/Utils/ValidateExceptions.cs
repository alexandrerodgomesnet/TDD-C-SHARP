using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio.Utils
{
    public class ValidateExceptions
    {
        private readonly List<string> _messages;
        public ValidateExceptions()
        {
            _messages = new List<string>();
        }

        public static ValidateExceptions New()
        {
            return new ValidateExceptions();
        }

        public ValidateExceptions When(bool existError, string message)
        {
            if (existError)
                _messages.Add(message);

            return this;
        }

        public void DisplayExceptions()
        {
            if (_messages.Any())
                throw new DomainException(_messages);
        }
    }    
}