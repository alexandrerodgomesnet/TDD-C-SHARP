using System;

namespace CursoOnline.Dominio.DTO
{
    public class MatriculaDTO
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public decimal Valor { get; set; }
    }
}