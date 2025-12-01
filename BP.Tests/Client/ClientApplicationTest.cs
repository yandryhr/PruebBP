using BP.Application.Dtos.Request;
using BP.Application.Interfaces;
using BP.Utilities.Static;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BP.Tests.Client
{
    [TestClass]
    public class ClientApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize(TestContext _testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task RegisterClient_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory!.CreateScope();
            var context = scope.ServiceProvider.GetService<IClientApplication>();

            //Arrange
            var contrasena = "";
            var estado = true;
            var nombre = "";
            var genero = "M";
            var edad = 36;
            var identificacion= "";
            var direccion = "San Calos";
            var telefono = "099888777";

            var expectedMessage = ReplyMessage.MESSAGE_VALIDATE;

            //Act
            var result = await context!.RegisterClient(new ClientRequestDto()
            {
                Contrasena = contrasena,
                Estado = estado,
                Nombre = nombre,
                Genero = genero,
                Edad = edad,
                Identificacion = identificacion,
                Direccion = direccion,
                Telefono = telefono
            });

            var current = result.Message;

            Console.WriteLine(result.Message);
            //Assert
            Assert.AreEqual(expectedMessage, current);
        }

        [TestMethod]
        public async Task RegisterClient_WhenSendingCorrectValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory!.CreateScope();
            var context = scope.ServiceProvider.GetService<IClientApplication>();

            //Arrange
            var contrasena = "123";
            var estado = true;
            var nombre = "Yandry";
            var genero = "M";
            var edad = 36;
            var identificacion = "1104799737";
            var direccion = "San Calos";
            var telefono = "099888777";

            var expectedMessage = ReplyMessage.MESSAGE_SAVE;

            //Act
            var result = await context!.RegisterClient(new ClientRequestDto()
            {
                Contrasena = contrasena,
                Estado = estado,
                Nombre = nombre,
                Genero = genero,
                Edad = edad,
                Identificacion = identificacion,
                Direccion = direccion,
                Telefono = telefono
            });

            Console.WriteLine(result.Message);
            var current = result.Message;


            //Assert
            Assert.AreEqual(expectedMessage, current);
        }

    }
}
