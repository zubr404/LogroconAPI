using System;

namespace LogroconAPI.Models.RequestModels
{
    public class NewEmployeeRequest
    {
        public string EmployeeFullName { get; set; }
        public DateTime EmployeeBirthDate { get; set; }
        public int PositionID { get; set; }
    }
}
