using Xunit;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Utils;
using CursoOnline.Dominio.Shared;
using CursoOnline.Utils.Test;

namespace CursoOnline.Dominio.Test
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
