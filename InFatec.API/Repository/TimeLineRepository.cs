using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Repository
{
    public class TimeLineRepository : ITimeLineRepository
    {
        private readonly IMapper _mapper;
        private readonly SqlServerContext _context;

        public TimeLineRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TimeLineDTO>> GetAllTimeLine()
        {
            var result = await _context.TimeLine.ToListAsync();
            return _mapper.Map<List<TimeLineDTO>>(result);
        }

        public async Task<TimeLineDTO> InsertTimeLine(TimeLineDTO timeLineDTO)
        {
            var model = _mapper.Map<TimeLine>(timeLineDTO);
            await _context.TimeLine.AddAsync(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<TimeLineDTO>(model);
        }
    }
}
