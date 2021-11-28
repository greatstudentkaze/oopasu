using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAssignmentManager.Domain
{
    public class AssignmentData
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        [Column(TypeName = "jsonb")]
        public EditorJSData Data { get; set; }
        
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public List<StudyAssignment> Assignments { get; set; }
    }
}