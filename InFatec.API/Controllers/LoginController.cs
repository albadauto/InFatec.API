using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using InFatec.API.Services;
using InFatec.API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _repository;
        public LoginController(ILoginRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ApiLoginDTO>> CreateNewUser([FromBody] ApiLoginDTO login)
        {
            try
            {
                login.Password = new CryptoUtil(SHA256.Create()).hashPassword(login.Password);
                await _repository.InsertNewUser(login);
                return Ok(new { success = true, data = login });
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }
        }


        [HttpPost("LoginUser")]
        public async Task<ActionResult<ApiLoginDTO>> LoginUser([FromBody] ApiLoginDTO login)
        {
            try
            {
                if (login.RA == null || login.Password == null) return BadRequest();
                var result = await _repository.FindUserByRA(login.RA, new CryptoUtil(SHA256.Create()).hashPassword(login.Password));
                if (result != null)
                {
                    var token = TokenService.GenerateToken();
                    return Ok(new { logged = true, bearer = token });

                }
                else
                {
                    return NotFound(new { logged = false, message = "Usuário ou senha inválido(s)"}) ;
                }
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }
        }
    }
}
