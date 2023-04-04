using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;

namespace InFatec.API.Repository
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly SqlServerContext _context;
        private IMapper _mapper;
        public CoursesRepository(SqlServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoursesDTO> InsertNewCourse(List<CoursesDTO> dto)
        {
            var model = _mapper.Map<Courses>(dto);
            await _context.Courses.AddAsync(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<CoursesDTO>(model);
        }
    }
}
