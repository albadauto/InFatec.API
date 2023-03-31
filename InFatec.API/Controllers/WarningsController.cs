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

        [Authorize]
        [HttpPost("CreateNewWarning")]
        public async Task<ActionResult<WarningDTO>> CreateNewWarning([FromForm] WarningDTO dto)
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
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }


    }
}
