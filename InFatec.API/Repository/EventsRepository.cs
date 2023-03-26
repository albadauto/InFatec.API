using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Repository
{
    public class EventsRepository : IEventsRepository
    {
        private readonly SqlServerContext _context;
        public IMapper _mapper;

        public EventsRepository(SqlServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteEvent(int Id)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.Id == Id);
            if (result != null)
            {
                _context.Events.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<EventsDTO>> GetAllEvents()
        {
            var result = await _context.Events.FindAsync();
            return _mapper.Map<IEnumerable<EventsDTO>>(result);
        }

        public async Task<EventsDTO> GetEventById(int Id)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<EventsDTO>(result);
        }

        public async Task<EventsDTO> InsertEvent(EventsDTO value)
        {
            var retorno = _mapper.Map<Events>(value);
            await _context.Events.AddAsync(retorno);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventsDTO>(retorno);
        }

        public async Task<EventsDTO> UpdateEvent(EventsDTO e)
        {
            var result = _mapper.Map<Events>(e);
            _context.Events.Update(result);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventsDTO>(result);
        }
    }
}
