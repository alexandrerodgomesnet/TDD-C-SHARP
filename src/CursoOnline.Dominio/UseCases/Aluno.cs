using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Utils;

namespace CursoOnline.Dominio.UseCases
{
    public class Aluno
    {        
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }

        public Aluno(string nome, string email, string cpf, PublicoAlvo publicoAlvo)
        {            
            ValidateExceptions
                .New()
                .When(string.IsNullOrEmpty(nome), Resources.NomeAlunoInvalido)
                .When(string.IsNullOrEmpty(email) || !email.EmailIsValid(), Resources.EmailInvalido)
                .When(string.IsNullOrEmpty(cpf) || !cpf.CpfIsValid(), Resources.CpfInvalido)
                .DisplayExceptions();

            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }
    }
}