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

        [HttpPost("InsertNewCourseByXLS")]
        public async Task<ActionResult<CoursesDTO>> InsertNewCourseByXLS()
        {
            FileInfo existingFile = new FileInfo("Storage/teste.xlsx");
            var courses = new List<CoursesDTO>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                Console.WriteLine(rowCount);
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

            foreach(var course in courses)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine(course.Period);
                Console.WriteLine(course.Coordinator);
            }


            return Ok();


        }

    }

}










