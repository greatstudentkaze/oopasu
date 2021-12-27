using System;

namespace StudyAssignmentManager.Domain
{
    public class EducationalMaterialDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EditorJSData Content { get; set; }
        public Guid AuthorId { get; set; }
    }
}