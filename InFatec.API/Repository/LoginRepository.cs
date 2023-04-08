using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private SqlServerContext _context;
        private IMapper _mapper;
        public LoginRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> DeleteByUserId(int id)
        {
            var result = await _context.Login.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return false;
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginDTO> FindUserByRA(string RA, string password)
        {
            var result = await _context.Login.FirstOrDefaultAsync(x => x.RA == RA && x.Password == password);
            return _mapper.Map<LoginDTO>(result);
        }

        public async Task<ApiLoginDTO> InsertNewUser(ApiLoginDTO dto)
        {
            Login login = _mapper.Map<Login>(dto);
            await _context.Login.AddAsync(login);
            await _context.SaveChangesAsync();
            return _mapper.Map<ApiLoginDTO>(login);
        }

       
    }
}
