using System;
using System.Collections.Generic;

namespace StudyAssignmentManager.Domain
{
    public class Answer
    {
        public Guid Id { get; set; }
        public List<String> Comments { get; set; }
        public List<String> AttachmentUrls { get; set; }
        public String EditorJSData { get; set; }
        
        public Guid AssignmentId { get; set; }
        public StudyAssignment Assignment { get; set; }
        public CheckRequest CheckRequest { get; set; }
    }
}