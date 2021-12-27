using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAssignmentManager.Domain
{
    [NotMapped]
    public class EditorJSData
    {
        public long Time { get; set; }
        public EditorJSBlock[] Blocks { get; set; }
        public string Version { get; set; }
    }
}