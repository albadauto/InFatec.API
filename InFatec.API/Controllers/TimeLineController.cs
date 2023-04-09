using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpGet("GetTimeLines")]
        public async Task<ActionResult<List<TimeLineDTO>>> GetTimeLines()
        {
            try
            {
                var data = await _repository.GetAllTimeLine();
                if (data != null) return Ok(new { success = true, result = data });
                else return NotFound(new { success = false, message = "Não há dados" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpDelete("DeleteTimeLine/{Id}")]
        public async Task<ActionResult> DeleteTimeLine(int Id)
        {
            try
            {
                var result = await _repository.DeleteTimeLine(Id);
                if (result) return Ok(new { success = true, message = "Deletado com sucesso" });
                else return NotFound(new { success = false, message = "Não encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpPut("UpdateOneTimeLine")]
        public async Task<ActionResult> UpdateOneTimeLine([FromBody] TimeLineDTO dto)
        {
            try
            {
                await _repository.UpdateTimeLine(dto);
                return Ok(new { message = "Atualizado com sucesso", success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }
    }
}
