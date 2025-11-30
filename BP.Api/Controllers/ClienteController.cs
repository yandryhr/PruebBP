using BP.Application.Dtos.Request;
using BP.Application.Interfaces;
using BP.Infrastructure.Commons.Bases.Request;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClientApplication _clientApplication;
        public ClienteController(IClientApplication clientApplication)
        {
            _clientApplication = clientApplication;
        }

        [HttpPost]
        public async Task<IActionResult> listClients([FromBody] BaseFiltersRequest filters)
        {
            var response = await _clientApplication.ListClients(filters);
            return Ok(response);
        }

        [HttpGet("{ClientId:long}")]
        public async Task<IActionResult> ClientById(long ClientId)
        {
            var response = await _clientApplication.ClientById(ClientId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterClient([FromBody] ClientRequestDto clientRequestDto)
        {
            var response = await _clientApplication.RegisterClient(clientRequestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{clientId:long}")]
        public async Task<IActionResult> EditClient(long clientId, [FromBody] ClientRequestDto clientRequestDto)
        {
            var response = await _clientApplication.EditClient(clientId, clientRequestDto);
            return Ok(response);
        }

        [HttpDelete("Remove/{clientId:long}")]
        public async Task<IActionResult> RemoveClient(long clientId)
        {
            var response = await _clientApplication.RemoveClient(clientId);
            return Ok(response);
        }

    }
}
