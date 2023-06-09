﻿using InFatec.API.DTO;
using InFatec.API.Model;

namespace InFatec.API.Repository.Interfaces
{
    public interface IWarningsRepository
    {
        Task<WarningDTO> InsertNewWarning(WarningDTO dto);
        Task<List<WarningDTO>> GetAllWarnings();
        Task<bool> DeleteWarning(int Id);
        Task<WarningDTO> GetLastWarning();
    }
}
