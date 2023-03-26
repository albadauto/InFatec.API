using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository _repository;
  
        public EventsController(IEventsRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpPost("InsertNewEvent")]
        public async Task<ActionResult<EventsDTO>> InsertNewEvent([FromBody] EventsDTO values)
        {
            try
            {
                var result = await _repository.InsertEvent(values);
                return Ok(new { success = true, data = result });
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        
        [Authorize]
        [HttpDelete("DeleteEvent/{Id}")]
        public async Task<ActionResult<EventsDTO>> DeleteEvent(int Id)
        {
            try
            {
                var result = await _repository.DeleteEvent(Id);
                return Ok(new { success = true, message = "Evento excluído com sucesso" });
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        [Authorize]
        [HttpGet("GetEvents")]
        public async Task<ActionResult<IEnumerable<EventsDTO>>> GetEvents()
        {
            try
            {
                var result = await _repository.GetAllEvents();
                if (result != null) return Ok(new { success = true, data = result });
                else return NotFound(new { success = false, message = "Nenhum registro encontrado" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetEventById/{Id}")]
        public async Task<ActionResult<IEnumerable<EventsDTO>>> GetEventById(int Id)
        {
            try
            {
                var result = await _repository.GetEventById(Id);
                if (result != null) return Ok(new { success = true, data = result });
                else return NotFound(new { success = false, message = "Nenhum registro encontrado" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("UpdateEvent")]
        public async Task<ActionResult<EventsDTO>> UpdateEvent([FromBody] EventsDTO dto)
        {
            try
            {
                var result = await _repository.UpdateEvent(dto);    
                if (result != null) return Ok(new { success = true, data = result, message = "Registro atualizado com sucesso" });
                else return NotFound(new { success = false, message = "Nenhum registro encontrado" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
