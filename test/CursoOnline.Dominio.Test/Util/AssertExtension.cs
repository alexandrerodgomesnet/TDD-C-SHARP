using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Util
{
	public static class AssertExtension
	{
		public static void ComMensagem(this ArgumentException exception, string mensagem)
		{
			bool mensagemIgual = exception.Message == mensagem;
			string mensagemFormatada = $"Esperava a mensagem: '{mensagem}' mas foi encontrada a mensagem '{exception.Message}'";
			string mensageErro = !mensagemIgual ? mensagemFormatada : string.Empty;
			Assert.True(mensagemIgual, mensageErro);
		}
	}
}