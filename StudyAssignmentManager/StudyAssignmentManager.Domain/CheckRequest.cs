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
        public DateTime CreationDate { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid ReviewerId { get; set; }
    }
}