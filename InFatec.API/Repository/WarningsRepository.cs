using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Repository
{
    public class WarningsRepository : IWarningsRepository
    {
        private IMapper _mapper;
        private readonly SqlServerContext _context;
        public WarningsRepository(IMapper mapper, SqlServerContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WarningDTO> InsertNewWarning(WarningDTO dto)
        {
            var mapped = _mapper.Map<Warnings>(dto);
            await _context.Warnings.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return _mapper.Map<WarningDTO>(mapped);
        }

        public async Task<List<WarningDTO>> GetAllWarnings()
        {
            var Warning = new List<Warnings>();
            var result = await (from w in _context.Warnings
                          join u in _context.Login
                          on w.LoginId equals u.Id
                          select new { w.Message, u.Name, u.Email, u.RA, w.ImageName, w.ImgUri, w.Title }).ToListAsync();
            foreach (var value in result)
            {
                Warning.Add(new Warnings
                {
                    Message = value.Message,
                    ImgUri = value.ImgUri,
                    ImageName = value.ImageName,
                    Login = new Login
                    {
                        Name = value.Name,
                        Email = value.Email,
                        RA = value.RA
                    },
                    Title = value.Title
                });
            }
            return _mapper.Map<List<WarningDTO>>(Warning);
        }

        public async Task<bool> DeleteWarning(int Id)
        {
            var result = await _context.Warnings.FirstOrDefaultAsync(x => x.Id == Id);
            if(result != null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<WarningDTO> GetLastWarning()
        {
            var result = await (from w in _context.Warnings
                               join u in _context.Login
                               on w.LoginId equals u.Id
                               select new { w.Message, u.Name, u.Email, u.RA, w.ImageName, w.ImgUri, w.Id }).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            var Warning = new Warnings
            {
                Message = result.Message,
                Login = new Login
                {
                    Name = result.Name,
                    Email = result.Email,
                    RA = result.RA,
                },
                ImageName = result.ImageName,
                ImgUri = result.ImgUri,
            };

            return _mapper.Map<WarningDTO>(Warning);
        }
    }
}
