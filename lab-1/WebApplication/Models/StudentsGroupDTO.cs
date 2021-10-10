using System;

namespace WebApplication.Models
{
    public class StudentsGroupDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int Course { get; set; }
        public int StudentsCount { get; set; }
        public string Department { get; set; }
        public string GraduatingDepartment { get; set; }
        public string GroupLeaderFullName { get; set; }
        public bool IsGraduated { get; set; }
    }
}