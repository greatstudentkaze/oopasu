using System;

namespace StudyAssignmentManager.Domain
{
    public enum CheckRequestStatus
    {
        Completed,
        Canceled,
        InProgress,
    }
    
    public class CheckRequest
    {
        public Guid Id { get; set; }
        public CheckRequestStatus Status { get; set; }
        public int Number { get; set; }
        public DateTime CreationDate { get; set; }
        
        public Guid AssignmentId { get; set; }
        public StudyAssignment Assignment { get; set; }
        public Guid ReviewerId { get; set; }
        public User Reviewer { get; set; }
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}