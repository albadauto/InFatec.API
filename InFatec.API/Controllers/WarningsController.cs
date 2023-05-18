using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarningsController : ControllerBase
    {
        private readonly IWarningsRepository _repository;
        public WarningsController(IWarningsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateNewWarning")]
        public async Task<ActionResult<WarningDTO>> CreateNewWarning([FromForm] WarningDTO dto)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString() + new DateTime().Hour + "_" + dto.ImageFile.FileName;
                var path = Path.Combine("Storage/", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }
                var result = await _repository.InsertNewWarning(new WarningDTO
                {
                    LoginId = dto.LoginId,
                    ImgUri = path,
                    Message = dto.Message,
                    ImageName = fileName
                });
                return Ok(new { success = true, data = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = "Erro: solicitação inválida", exception = ex.Message, stacktrace = ex.StackTrace });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { sucess = false, message = "Erro interno do servidor", exception = ex.Message, stacktrace = ex.StackTrace });
            }



        }

        //Função para download de imagem via API
        [HttpGet("ShowImage/{filename}")]
        public async Task<FileResult> ShowImage(string filename)
        {

            var filePath = $"Storage/{filename}";

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, "image/*", Path.GetFileName(filePath));


        }

        [Authorize]
        [HttpGet("GetAllWarnings")]
        public async Task<ActionResult<IEnumerable<WarningDTO>>> GetAllWarnings()
        {
            try
            {
                var result = await _repository.GetAllWarnings();
                if (result == null) return NotFound(new { success = false, message = "Nenhum registro encontrado" });
                return Ok(new { success = true, data = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = "Erro: solicitação inválida", exception = ex.Message, stacktrace = ex.StackTrace });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { sucess = false, message = "Erro interno do servidor", exception = ex.Message, stacktrace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpDelete("DeleteWarning/{Id}")]
        public async Task<ActionResult> DeleteWarning(int Id)
        {
            try
            {
                var result = await _repository.DeleteWarning(Id);
                if (result) return Ok(new { success = true, message = "Aviso deletado com sucesso" });
                else return NotFound(new { success = false, message = "Erro: Aviso, não encontrado" });
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(new { success = false, message = "Erro: solicitação inválida", exception = ex.Message, stacktrace = ex.StackTrace });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { sucess = false, message = "Erro interno do servidor", exception = ex.Message, stacktrace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpGet("GetLastWarnings")]
        public async Task<ActionResult<WarningDTO>> GetLastWarnings()
        {
            try
            {
                var result = await _repository.GetLastWarning();
                if(result != null) return Ok(new { success = true, data = result });
                else return NotFound(new { success = false, message = "Erro: Avisos, não encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(406, new { success = false, message = ex.Message, exception = ex.StackTrace });
            }
        }


    }
}
