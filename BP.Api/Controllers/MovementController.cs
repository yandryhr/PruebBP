using BP.Appication.Dtos.Request;
using BP.Appication.Interfaces;
using BP.Application.Dtos.Request;
using BP.Application.Interfaces;
using BP.Application.Services;
using BP.Infrastructure.Commons.Bases.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementApplication _movementApplication;
        public MovementController(IMovementApplication clientApplication)
        {
            _movementApplication = clientApplication;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterClient([FromBody] MovementRequestDto movementRequestDto)
        {
            var response = await _movementApplication.RegisterMovement(movementRequestDto);
            return Ok(response);
        }

        [HttpPost("reporte/pdf")]
        public async Task<IActionResult> GetAccountStatusPdf([FromBody] AccounStatusRequest request)
        {
            var data = await _movementApplication.AccountStatusPdf(request);

            if (data == null || !data.Any())
                return BadRequest("No existen datos para el reporte");

            byte[] pdfBytes = QuestPdfHelper.GenerateAccountStatusPdf(data);

            return File(pdfBytes, "application/pdf", "EstadoCuenta.pdf"); 
        }
    }
}
