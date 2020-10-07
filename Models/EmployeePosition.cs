namespace LogroconAPI.Models
{
    public class EmployeePosition
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
