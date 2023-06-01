using InFatec.API.DTO;

namespace InFatec.API.Repository.Interfaces
{
    public interface ICoursesRepository
    {
        Task<CoursesDTO> InsertNewCourse(CoursesDTO dto);
        Task<List<CoursesDTO>> ListAllCourses();
        Task<bool> TruncateAllCourses();
        Task<EditCoursesDTO> UpdateCourse(EditCoursesDTO dto);
    }
}
