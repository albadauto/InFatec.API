using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ITimeLineRepository
    {
        Task<TimeLineDTO> InsertTimeLine(TimeLineDTO timeLineDTO);
        Task<List<TimeLineDTO>> GetAllTimeLine();
        Task<bool> DeleteTimeLine(int Id);
        Task<TimeLineDTO> UpdateTimeLine(TimeLineDTO dto);
        
    }
}
