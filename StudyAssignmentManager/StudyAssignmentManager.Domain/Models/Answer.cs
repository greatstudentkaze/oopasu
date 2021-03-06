using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAssignmentManager.Domain
{
    public class Answer
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "jsonb")]
        public EditorJSData Content { get; set; }
        
        public Guid AssignmentId { get; set; }
        public StudyAssignment Assignment { get; set; }
        public CheckRequest CheckRequest { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}