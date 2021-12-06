using System;

namespace StudyAssignmentManager.Domain
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}