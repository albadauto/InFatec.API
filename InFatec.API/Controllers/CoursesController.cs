using Azure;
using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
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
                    courses.Add(course);
                }
            }

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine(course.Period);
                Console.WriteLine(course.Coordinator);
            }
            return courses;
        }



        [HttpPost("InsertNewCourseByXLSX")]
        public async Task<ActionResult<CoursesDTO>> InsertNewCourseByXLSX([FromForm] CoursesDTO dto)
        {
            try
            {

                var name = dto.Excel.FileName + new DateTime().Hour.ToString();
                var filepath = Path.Combine("Storage/", name);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    await dto.Excel.CopyToAsync(fileStream);
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
                    });
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}










