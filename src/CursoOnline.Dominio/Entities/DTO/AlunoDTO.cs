using System;

namespace CursoOnline.Dominio.DTO
{
    public class AlunoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string PublicoAlvo { get; set; }
    }
}