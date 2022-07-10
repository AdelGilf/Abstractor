using Contracts.DTO;
using Core.Services.Abstraction;
using FluentResultExtension;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IUserService _userService;
        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Registration(RegistrationDTO registrationDTO)
        {
            var result = await _userService.RegistrationAsync(registrationDTO);
            return result.IsSuccess ? Ok(result?.Value) : BadRequest(result.GetErrors());
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _userService.LoginAsync(loginDTO);
            return result.IsSuccess ? Ok(result?.Value) : BadRequest(result.GetErrors());
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete()
        {
            var result = await _userService.DeleteUserAsync();
            return result.IsSuccess ? Ok() : BadRequest(result.GetErrors());
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<ActionResult> UpdatePassword(string password)
        {
            var result = await _userService.UpdatePasswordAsync(password);
            return result.IsSuccess ? Ok() : BadRequest(result.GetErrors());
        }

        [HttpPut]
        [Route("UpdateEmail")]
        public async Task<ActionResult> UpdateEmail(string email)
        {
            var result = await _userService.UpdateEmailAsync(email);
            return result.IsSuccess ? Ok() : BadRequest(result.GetErrors());
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult> UpdateUser(UserDTO userDTO)
        {
            var result = await _userService.UpdateUserAsync(userDTO);
            return result.IsSuccess ? Ok() : BadRequest(result.GetErrors());
        }
    }
}
