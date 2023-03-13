using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ILoginRepository
    {

        Task<ApiLoginDTO> InsertNewUser(ApiLoginDTO dto);

        Task<ApiLoginDTO> FindUserByRA(string RA, string password);
        Task<bool> DeleteByUserId(int id);
    }
}
