using BP.Appication.Dtos.Request;
using BP.Appication.Interfaces;
using BP.Utilities.Static;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BP.Tests.Account
{
    [TestClass]
    public class AccountApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null!;
        private static IServiceScopeFactory? _scopeFactory = null!;
        public static void Initialize(TestContext _testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task RegisterAccount_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory!.CreateScope();
            var context = scope!.ServiceProvider.GetService<IAccountApplication>();

            //Arrange
            var numeroCuenta = 132456;
            var tipoCuenta = "Ahorro";
            var saldoInicial = 22;
            var estado = true;
            var clienteId = 4;            

            var expectedMessage = ReplyMessage.MESSAGE_SAVE;

            //Act
            var result = await context!.RegisterAccount(new AccountRequestDto()
            {
                ClienteId = clienteId,
                Estado = estado,
                NumeroCuenta = numeroCuenta,
                SaldoInicial = saldoInicial,
                TipoCuenta = tipoCuenta
            });
            Console.WriteLine(result.Message);
            var current = result.Message;


            //Assert
            Assert.AreEqual(expectedMessage, current);
        }

    }
}
