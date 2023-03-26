using AutoMapper;
using InFatec.API.Context;
using InFatec.API.DTO;
using InFatec.API.Model;
using InFatec.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Repository
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        private IMapper _mapper;
        private readonly SqlServerContext _context;
        public ForgotPasswordRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ResetPasswordDTO> ResetPassword(ResetPasswordDTO dto)
        {
            var result = await _context.Login.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if(result != null)
            {
                result.Password = dto.Password;
                _context.Login.Update(result);
                await _context.SaveChangesAsync();
                return _mapper.Map<ResetPasswordDTO>(result);
            }
            else
            {
                return null;
            }
           
        }

        public async Task<CodeDTO> InsertNewCode(CodeDTO code)
        {
            var model = _mapper.Map<Code>(code);
            await _context.Code.AddAsync(model);
            return _mapper.Map<CodeDTO>(model);
        }
    }
}
