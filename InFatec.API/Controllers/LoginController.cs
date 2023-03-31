using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using InFatec.API.Services;
using InFatec.API.Util;
using Microsoft.AspNetCore.Authorization;
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
                return Ok(new { success = true, data = new { name = login.Name, RA = login.RA} });
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }
        }



        [HttpPost("LoginUser")]
        public async Task<ActionResult<LoginDTO>> LoginUser([FromBody] LoginDTO login)
        {
            try
            {
                if (login.RA == null || login.Password == null) return BadRequest(new { logged = false  }) ;
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

        [Authorize]
        [HttpDelete("DeleteUser/{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            try
            {
                var result = await _repository.DeleteByUserId(Id);
                if (result) return Ok(new { success = true, message = "Deletado com sucesso"});
                else return BadRequest(new { success = false, message = "Erro ao tentar a exclusão desse usuário"});
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }
        }

    }
}
