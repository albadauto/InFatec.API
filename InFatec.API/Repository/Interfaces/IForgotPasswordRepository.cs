using InFatec.API.DTO;
using InFatec.API.Model;

namespace InFatec.API.Repository.Interfaces
{
    public interface IForgotPasswordRepository
    {
        Task<ResetPasswordDTO> ResetPassword(ResetPasswordDTO dto);
        Task<CodeDTO> InsertNewCode(CodeDTO code);

        Task<CodeDTO> VerifyCode(CodeDTO code);
    }
}
