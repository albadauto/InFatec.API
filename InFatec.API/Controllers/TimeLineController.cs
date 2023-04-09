using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLineController : ControllerBase
    {
        private readonly ITimeLineRepository _repository;
        public TimeLineController(ITimeLineRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("InsertNewTimeLine")]
        public async Task<ActionResult> InsertNewTimeLine([FromBody] TimeLineDTO dto)
        {
            try
            {
                await _repository.InsertTimeLine(dto);
                return Ok(new { message = "Inserido com sucesso", success = true});


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }
    }
}
