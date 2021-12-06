using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAssignmentManager.Domain
{
    [NotMapped]
    public class EditorJSData
    {
        public List<EditorJSBlock> Blocks { get; set; }
    }
}