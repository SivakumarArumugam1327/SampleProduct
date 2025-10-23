namespace Myproducts.Models
{
    public class Employee
    {


        public int? Id { get { return EmployeeId; } }
        public int? EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string?  PhoneNumber { get; set; }
        public string? Department { get; set; }
        public decimal? Salary { get; set; }
        public DateTime?  DateOfJoining { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}