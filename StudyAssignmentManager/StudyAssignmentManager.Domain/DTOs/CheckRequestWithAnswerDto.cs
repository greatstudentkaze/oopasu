using System;
using System.Collections.Generic;

namespace StudyAssignmentManager.Domain
{
    public class CheckRequestWithAnswerDto
    {
        public Guid AssignmentId { get; set; }
        public Guid ReviewerId { get; set; }
        public CheckRequestAnswerDto Answer { get; set; }
        
    }
    
    public class CheckRequestAnswerDto
    {
        public List<string> AttachmentUrls { get; set; }
        public EditorJSData Data { get; set; }
    }
}