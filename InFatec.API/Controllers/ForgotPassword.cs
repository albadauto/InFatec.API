using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPassword : ControllerBase
    {
        private readonly IForgotPasswordRepository _repository;
        public ForgotPassword(IForgotPasswordRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpPut("NewPassword")]
        public async Task<ActionResult> NewPassword([FromBody] ResetPasswordDTO dto)
        {
            try
            {
                var result = await _repository.ResetPassword(dto);
                if (result == null) return BadRequest( new { success = false });
                return Ok(new { success = true, data = dto }); 
            }
            catch (Exception err)
            {

                return BadRequest(new { success = false, Error = err.Message });
            }
        }

    }
}
