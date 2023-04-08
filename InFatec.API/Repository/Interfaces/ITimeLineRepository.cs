using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ITimeLineRepository
    {
        Task<TimeLineDTO> InsertTimeLine(TimeLineDTO timeLineDTO);  
        
    }
}
