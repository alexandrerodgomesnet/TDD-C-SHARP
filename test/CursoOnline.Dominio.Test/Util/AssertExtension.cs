using CursoOnline.Dominio.Utils;
using Xunit;

namespace CursoOnline.Dominio.Test.Util
{
    public static class AssertExtension
	{
		public static void ComMensagem<T>(this GenericExceptions<T> exception, string mensagem)
		{
			bool mensagemIgual = exception.Message == mensagem;
			string mensagemFormatada = $"Esperava a mensagem: '{mensagem}' mas foi encontrada a mensagem '{exception.Message}'";
			string mensageErro = !mensagemIgual ? mensagemFormatada : string.Empty;
			Assert.True(mensagemIgual, mensageErro);
		}

		public static void ComMensagem(this DomainException exception, string mensagem)
		{
			bool contemMensagem = exception.Messages.Contains(mensagem);
			string mensagemFormatada = $"Esperava a mensagem: '{mensagem}' mas foi encontrada a mensagem '{exception.Message}'";
			string mensageErro = !contemMensagem ? mensagemFormatada : string.Empty;
			Assert.True(contemMensagem, mensageErro);
		}
	}
}