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

        public async Task<bool> DeleteTimeLine(int Id)
        {
            var result = await _context.TimeLine.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null) return false;
            _context.TimeLine.Remove(result);
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<TimeLineDTO> UpdateTimeLine(TimeLineDTO dto)
        {
            var mapped = _mapper.Map<TimeLine>(dto);
            _context.TimeLine.Update(mapped);
            await _context.SaveChangesAsync();
            return _mapper.Map<TimeLineDTO>(mapped);
        }
    }
}
