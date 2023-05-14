using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ILoginRepository
    {

        Task<ApiLoginDTO> InsertNewUser(ApiLoginDTO dto);

        Task<LoginDTO> FindUserByRA(string RA, string password);
        Task<bool> DeleteByUserId(int id);

        Task<ApiLoginDTO> UpdateUser(ApiLoginDTO dto);
        Task<ApiLoginDTO> FindRA(string RA);
    }
}
