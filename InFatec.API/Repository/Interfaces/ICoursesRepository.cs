﻿using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ICoursesRepository
    {
        Task<CoursesDTO> InsertNewCourse(List<CoursesDTO> dto);
    }
}