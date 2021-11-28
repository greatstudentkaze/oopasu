using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyAssignmentManager.Domain
{
    public class StudyAssignment
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "Укажите дату истечения срока задания")]
        public DateTime? DueDate { get; set; }
        public Boolean IsCompleted { get; set; }
        
        [Required]
        public Guid DataId { get; set; }
        public AssignmentData Data { get; set; }
        public Guid StudentId { get; set; }
        public User Student { get; set; }
        public Guid TeacherId { get; set; }
        public User Teacher { get; set; }
        public Guid TutorId { get; set; }
        public User Tutor { get; set; }
        public List<CheckRequest> CheckRequests { get; set; }
        public List<Answer> Answers { get; set; }
    }
}