using System;

namespace CursoOnline.Dominio.DTO
{
    public class CursoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}