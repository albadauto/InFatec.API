using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using InFatec.API.Util.Interfaces;
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
        private readonly IEmailUtil _email;
        private string Subject = "(NÃO RESPONDA) INFATEC ";
        public ForgotPassword(IForgotPasswordRepository repository, IEmailUtil email)
        {
            _email = email;
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

        [Authorize]
        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail([FromBody] string body, [FromBody] string email)
        {
            try
            {
                var result = await _email.SendEmail(email, this.Subject, body);
                if (result)
                    return Ok(new { success = true, message = "Email enviado com sucesso, verificar caixa de entrada" });
                else
                    return BadRequest(new { success = false, message = "Erro ao enviar email" });
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
    }
}
