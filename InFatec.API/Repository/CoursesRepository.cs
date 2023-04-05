using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CoursesDTO> InsertNewCourse(CoursesDTO dto)
        {
            var model = _mapper.Map<Courses>(dto);
            await _context.Courses.AddAsync(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<CoursesDTO>(model);
        }

        public async Task<List<CoursesDTO>> ListAllCourses()
        {
            var result = await _context.Courses.ToListAsync();
            return _mapper.Map<List<CoursesDTO>>(result);
        }

        public async Task<bool> TruncateAllCourses()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE COURSES");
            return true;
        }
    }
}
