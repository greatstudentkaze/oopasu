using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAssignmentManager.Domain
{
    public class Answer
    {
        public Guid Id { get; set; }
        public List<String> Comments { get; set; }
        public List<String> AttachmentUrls { get; set; }
        
        [Column(TypeName = "jsonb")]
        public EditorJSData Data { get; set; }
        
        public Guid AssignmentId { get; set; }
        public StudyAssignment Assignment { get; set; }
        public CheckRequest CheckRequest { get; set; }
    }
}