using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Shared;
using CursoOnline.Dominio.Utils;

namespace CursoOnline.Dominio.UseCases
{
	public class Curso
	{
        #region Propriedades...
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }
        #endregion

        #region Construtor...
        public Curso(string nome, string descricao, decimal cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
        {
            ValidateExceptions
                .New()
                .When(string.IsNullOrEmpty(nome), Resources.NomeDoCursoInvalido)
                .When(cargaHoraria <= 0, Resources.CargaHorariaDoCursoInvalida)
                .When(valor <= 0, Resources.ValorDoCursoInvalido)
                .DisplayExceptions();

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
        #endregion

        #region Metodos...
        public void EditarNome(string nome)
        {
            ValidateExceptions
                .New()
                .When(string.IsNullOrEmpty(nome), Resources.NomeDoCursoInvalido)
                .DisplayExceptions();

            Nome = nome;
        }

        public void EditarCargaHoraria(decimal cargaHoraria)
        {
            ValidateExceptions
                .New()
                .When(cargaHoraria <= 0, Resources.CargaHorariaDoCursoInvalida)
                .DisplayExceptions();

            CargaHoraria = cargaHoraria;
        }

        public void EditarValor(decimal valor)
        {
            ValidateExceptions
                .New()
                .When(valor <= 0, Resources.ValorDoCursoInvalido)
                .DisplayExceptions();

            Valor = valor;
        }
        #endregion
    }
}