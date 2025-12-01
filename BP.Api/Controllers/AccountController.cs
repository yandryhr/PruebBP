using BP.Appication.Dtos.Request;
using BP.Appication.Interfaces;
using BP.Application.Dtos.Request;
using BP.Application.Interfaces;
using BP.Infrastructure.Commons.Bases.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountApplication _accountApplication;
        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [HttpPost]
        public async Task<IActionResult> listClients([FromBody] BaseFiltersRequest filters)
        {
            var response = await _accountApplication.ListAccounts(filters);
            return Ok(response);
        }

        [HttpGet("{AccountNum:long}")]
        public async Task<IActionResult> ClientById(long AccountNum)
        {
            var response = await _accountApplication.AccountByNum(AccountNum);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAccount([FromBody] AccountRequestDto clientRequestDto)
        {
            var response = await _accountApplication.RegisterAccount(clientRequestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{AccountNum:long}")]
        public async Task<IActionResult> EditClient(long AccountNum, [FromBody] AccountRequestDto accountRequestDto)
        {
            var response = await _accountApplication.EditAccount(AccountNum, accountRequestDto);
            return Ok(response);
        }

        [HttpDelete("Remove/{AccountNum:long}")]
        public async Task<IActionResult> RemoveClient(long AccountNum)
        {
            var response = await _accountApplication.DeleteAccount(AccountNum);
            return Ok(response);
        }
    }
}
