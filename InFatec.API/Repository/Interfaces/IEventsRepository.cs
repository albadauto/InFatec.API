using InFatec.API.DTO;
using InFatec.API.Model;

namespace InFatec.API.Repository.Interfaces
{
    public interface IEventsRepository
    {
        public Task<EventsDTO> InsertEvent(EventsDTO e);
        public Task<bool> DeleteEvent(int Id);
        public Task<EventsDTO> GetEventById(int Id);
        public Task<EventsDTO> UpdateEvent(EventsDTO e);
        public Task<IEnumerable<EventsDTO>> GetAllEvents();
        public Task<EventsDTO> GetLastEvent();
    }
}
