using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using InFatec.API.Util;
using InFatec.API.Util.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPassword : ControllerBase
    {
        private readonly IForgotPasswordRepository _repository;
        private readonly IEmailUtil _email;
        private string Subject = "(NÃO RESPONDA) INFATEC ";
        private string code;
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
                dto.Password = new CryptoUtil(SHA256.Create()).hashPassword(dto.Password);
                var result = await _repository.ResetPassword(dto);
                if (result == null) return BadRequest(new { success = false });
                return Ok(new { success = true, data = dto });
            }
            catch (Exception err)
            {

                return BadRequest(new { success = false, Error = err.Message });
            }
        }

        [HttpPost("SendEmail/{IdLogin}")]
        public async Task<ActionResult> SendEmail([FromBody] EmailDTO dto, int IdLogin)
        {

            Console.WriteLine(IdLogin);
                this.code = Guid.NewGuid().ToString();
                var result = await _email.SendEmail(dto.Email, this.Subject, dto.Body + " " + this.code);
                await _repository.InsertNewCode(new CodeDTO
                {
                    CodeString = this.code,
                    ApiLoginId = IdLogin
                });
                if (result)
                    return Ok(new { success = true, message = "Email enviado com sucesso, verificar caixa de entrada" });
                else
                    return BadRequest(new { success = false, message = "Erro ao enviar email" });
            



        }
    }
}
