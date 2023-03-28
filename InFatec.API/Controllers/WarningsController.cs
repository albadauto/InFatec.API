using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var path = Path.Combine("Storage", fileName);
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }
                var result = await _repository.InsertNewWarning(new WarningDTO
                {
                    ImgUri = path + fileName,
                    Message = dto.Message,
                });
                return Ok(new { success = true, data = result});
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
