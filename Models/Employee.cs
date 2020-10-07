using System;
using System.Collections.Generic;

namespace LogroconAPI.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public List<EmployeePosition> EmployeePositions { get; set; }

        public Employee()
        {
            EmployeePositions = new List<EmployeePosition>();
        }
    }
}
