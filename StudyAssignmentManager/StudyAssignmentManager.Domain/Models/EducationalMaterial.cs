using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAssignmentManager.Domain
{
    public class EducationalMaterial
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Название обязательно для заполнения")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Описание обязательно для заполнения")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Текст задания обязателен для заполнения")]
        [Column(TypeName = "jsonb")]
        public EditorJSData Content { get; set; }
        
        [Required]
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public List<StudyAssignment> Assignments { get; set; }
    }
}