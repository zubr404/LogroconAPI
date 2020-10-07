using System;
using System.Collections.Generic;

namespace LogroconAPI.ModelsDTO
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public List<PositionDto> Positions { get; set; }

        public EmployeeDto()
        {
            Positions = new List<PositionDto>();
        }
    }
}
