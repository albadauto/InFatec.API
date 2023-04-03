using InFatec.API.DTO;
using InFatec.API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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

            var file = new FileStream("Storage/Cursos.xls", FileMode.Open, FileAccess.Read);
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            List<object> values = new List<object>();

            ISheet sheet = workbook.GetSheetAt(0);
            for (var i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null && row.Cells.Count > 0)
                {
                    for (int j = row.FirstCellNum; j <= row.LastCellNum; j++)
                    {
                        ICell cell = row.GetCell(j);

                        if (cell != null)
                        {
                            values.Add(cell.ToString());
                        }
                        else
                        {
                            values.Add(null);
                        }

                    }
                }

            }
            //Continuar daqui
            var dtoTwo = new CoursesDTO();
           for(var i = 0; i <= values.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        dtoTwo.Name = values[i].ToString();
                        break;
                    case 1:
                        dtoTwo.Period = values[i].ToString();
                        break;
                    case 2:
                        dtoTwo.Start = TimeSpan.Parse(values[i].ToString());
                        break;
                    case 3:
                        dtoTwo.End = TimeSpan.Parse(values[i].ToString());
                        break;
                    case 4:
                        dtoTwo.Coordinator = values[i].ToString();
                    break;
                }
            }

            await _repository.InsertNewCourse(dtoTwo);




            return Ok();


        }
    }
}
