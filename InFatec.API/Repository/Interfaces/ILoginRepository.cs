using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ILoginRepository
    {

        Task<ApiLoginDTO> InsertNewUser(ApiLoginDTO dto);

        Task<LoginDTO> FindUserByEmail(string email, string password);
        Task<bool> DeleteByUserId(int id);

        Task<ApiLoginDTO> UpdateUser(ApiLoginDTO dto);
        Task<ApiLoginDTO> FindRA(string RA);
        Task<bool> verifyIfHasEmail(string email);
    }
}
