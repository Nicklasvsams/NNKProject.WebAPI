using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NNKProject.BLL.DTO;
using NNKProject.BLL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NNKProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{accountName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUsername([FromRoute] string accountName)
        {
            try
            {
                var accountResponse = await _accountService.GetAccountByNameAsync(accountName);

                if (accountResponse == null) return NotFound();

                return Ok(accountResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{accountName}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthorizeLogin([FromRoute] string accountName, [FromRoute] string password)
        {
            try
            {
                var accountResponse = await _accountService.AuthorizeAccountAsync(accountName, password);
                    
                if (accountResponse == null) return Unauthorized();

                return Ok(accountResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AccountRequest accReq)
        {
            try
            {
                var accountResponse = await _accountService.CreateAccountAsync(accReq);

                if (accountResponse == null) return StatusCode(500);

                return Ok(accountResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{accountName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] AccountRequest accReq, [FromRoute] string accountName)
        {
            try
            {
                var accountResponse = await _accountService.UpdateAccountByNameAsync(accReq, accountName);

                if (accountResponse == null) return NotFound();

                return Ok(accountResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{accountName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] string accountName)
        {
            try
            {
                var accountResponse = await _accountService.DeleteAccountSaveDataByNameAsync(accountName);

                if (accountResponse == null) return NotFound();

                return Ok(accountResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
