using Xunit;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Utils;
using CursoOnline.Dominio.Test.Util;
using CursoOnline.Dominio.Shared;

namespace CursoOnline.Dominio.Test.PublicoAlvoConversor
{
    public class PublicoAlvoTest
    {
        [Theory]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Estudantes, "Estudantes")]
        [InlineData(PublicoAlvo.Universitario, "Universitario")]
        public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvoEsperado, string publicoAlvo)
        {

            var publicoAlvoConvertido = publicoAlvo.ConverterPublicoAlvo();

            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Invalido";

            Assert.Throws<DomainException>(() => publicoAlvoInvalido.ConverterPublicoAlvo())
                .ComMensagem(Resources.PublicoAlvoInvalido);
        }
    }
}
