using System.Collections.Generic;

namespace LogroconAPI.Models
{
    public class Position
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Grad { get; set; }
        public List<EmployeePosition> EmployeePositions { get; set; }

        public Position()
        {
            EmployeePositions = new List<EmployeePosition>();
        }
    }
}
