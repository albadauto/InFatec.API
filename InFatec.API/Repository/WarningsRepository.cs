﻿using AutoMapper;
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

        public async Task<IEnumerable<WarningDTO>> GetAllWarnings()
        {
            var result = await _context.Warnings.ToListAsync();
            return _mapper.Map<IEnumerable<WarningDTO>>(result);
        }
    }
}