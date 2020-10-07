using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogroconAPI.ModelsDTO
{
    public class PositionDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Range(1, 15, ErrorMessage = "Недопустимый Grad")]
        public int Grad { get; set; }
        public List<EmployeeDto> Employees { get; set; }

        public PositionDto()
        {
            Employees = new List<EmployeeDto>();
        }
    }
}
