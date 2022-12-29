using System.Text.RegularExpressions;

namespace CursoOnline.Dominio.Utils
{
    public static class UtilsExtensions
    {
        public static bool EmailIsValid(this string email)
        {
            Regex emailRegex = new Regex(@"^([\w\.\.]+)@([\w\.]+)((\.(\w){2,3})+)$");
            return emailRegex.Match(email).Success;
        }

        public static bool CpfIsValid(this string cpf)
        {
             Regex cpfRegex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
            return cpfRegex.Match(cpf).Success;
        }
    }
}