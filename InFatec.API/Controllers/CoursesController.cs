using Azure;
using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace InFatec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesRepository _repository;
        public CoursesController(ICoursesRepository repository)
        {
            _repository = repository;
        }
        
        private List<CoursesDTO> MapXLSXInList(string filepath)
        {
            FileInfo existingFile = new FileInfo(filepath);
            var courses = new List<CoursesDTO>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 1; row <= rowCount; row++)
                {
                    var course = new CoursesDTO();
                    course.Name = worksheet.Cells[row, 1].Value.ToString();
                    course.Period = worksheet.Cells[row, 2].Value.ToString();
                    course.Start = worksheet.Cells[row, 3].GetValue<TimeSpan>();
                    course.End = worksheet.Cells[row, 4].GetValue<TimeSpan>();
                    course.Coordinator = worksheet.Cells[row, 5].Value.ToString();
                    course.Matter = worksheet.Cells[row, 6].Value.ToString();
                    course.Floor = worksheet.Cells[row, 7].Value.ToString();

                    courses.Add(course);
                }
            }

            return courses;
        }

        [Authorize]
        [HttpPost("InsertNewCourseByXLSX")]
        public async Task<ActionResult<CoursesDTO>> InsertNewCourseByXLSX([FromForm] InsertFileDTO upload)
        {
            try
            {
                var name = upload.file.FileName + new DateTime().Hour.ToString();
                var filepath = Path.Combine("Storage/", name);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    await upload.file.CopyToAsync(fileStream);
                }
                var list = MapXLSXInList(filepath);

                foreach (var value in list)
                {
                    await _repository.InsertNewCourse(new CoursesDTO
                    {
                        Name = value.Name,
                        Period = value.Period,
                        Coordinator = value.Coordinator,
                        Start = TimeSpan.Parse(value.Start.ToString().Split(".")[1]),
                        End = TimeSpan.Parse(value.End.ToString().Split(".")[1]),
                        Matter = value.Matter,
                        Floor = value.Floor,    
                    });
                }
                return Ok(new { success = true, message = "Cursos inseridos com sucesso"});
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<List<CoursesDTO>>> GetAllCourses()
        {
            try
            {
                var result = await _repository.ListAllCourses();
                if (result.Count > 0) return Ok(new { success = true, data = result });
                else return NotFound(new { success = false, message = "Não há dados" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }

        [Authorize]
        [HttpDelete("DeleteAllCourses")]
        public async Task<ActionResult> DeleteAllCourses()
        {
            try
            {
                await _repository.TruncateAllCourses();
                return Ok(new { success = true, message = "Todos os cursos deletados com sucesso"});
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { success = false, message = ex.Message, trace = ex.StackTrace });
            }
        }


    }

}










