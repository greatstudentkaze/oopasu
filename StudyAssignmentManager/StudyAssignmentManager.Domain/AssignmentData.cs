using System;
using System.Collections.Generic;

namespace StudyAssignmentManager.Domain
{
    public class AssignmentData
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String EditorJSData { get; set; }
        
        public List<StudyAssignment> Assignments { get; set; }
    }
}