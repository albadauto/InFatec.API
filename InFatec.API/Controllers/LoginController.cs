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
        private bool isMaster = false;
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
                var userFound = await _repository.FindRA(login.RA);
                if (login.Email.Split("@")[1] != "fatec.sp.gov.br")
                {
                    return StatusCode(406, new { success = false, message = "O email deverá ser com domínio @fatec.sp.gov.br. Favor contatar o administrador" });
                }
                if (isMaster)
                {
                    await _repository.InsertNewUser(login);
                    return Ok(new { success = true, message = "Usuário inserido com sucesso by: Master" });

                }
                else
                {
                    if (userFound != null)
                    {
                        await _repository.InsertNewUser(login);
                        return Ok(new { success = true, data = new { name = login.Name, RA = login.RA } });
                    }
                    else
                    {
                        return StatusCode(406, new { success = false, message = "RA não cadastrado no banco de dados" });
                    }
                }




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
                if (login.RA == null || login.Password == null) return BadRequest(new { logged = false });
                var result = await _repository.FindUserByRA(login.RA, new CryptoUtil(SHA256.Create()).hashPassword(login.Password));
                if (result != null)
                {
                    var token = TokenService.GenerateToken();
                    return Ok(new { logged = true, bearer = token });
                }
                else
                {
                    return NotFound(new { logged = false, message = "Usuário ou senha inválido(s)" });
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
                if (result) return Ok(new { success = true, message = "Deletado com sucesso" });
                else return BadRequest(new { success = false, message = "Erro ao tentar a exclusão desse usuário" });
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }
        }

        [HttpPut("UpdateOneUser")]
        public async Task<ActionResult<ApiLoginDTO>> UpdateOneUser([FromBody] ApiLoginDTO dto)
        {
            try
            {
                var result = await _repository.UpdateUser(dto);
                if (result != null) return Ok(new { success = true, message = "Usuário atualizado com sucesso" });
                else return BadRequest(new { success = false, message = "Usuário não encontrado" });
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }
        }

        [HttpPost("InsertNewRAFixed")]
        public async Task<ActionResult<ApiLoginDTO>> InsertNewRAFixed([FromBody] ApiLoginDTO dto)
        {
            try
            {
                this.isMaster = true;
                await CreateNewUser(dto);
                return Ok(new { success = true, message = "Usuário inserido com sucesso by: Master" });

            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, err = ex.Message });
            }

        }

    }
}
