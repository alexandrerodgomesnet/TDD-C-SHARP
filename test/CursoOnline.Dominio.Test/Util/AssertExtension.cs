using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Util
{
	public static class AssertExtension
	{
		public static void ComMensagem(this ArgumentException exception, string mensagem)
		{
			bool temErro = exception.Message == mensagem;
			string mensagemFormatada = $"Esperava a mensagem: '{mensagem}' mas foi encontrada a mensagem '{exception.Message}'";
			string mensageErro = !temErro ? mensagemFormatada : string.Empty;
			Assert.True(temErro, mensageErro);
		}
	}
}