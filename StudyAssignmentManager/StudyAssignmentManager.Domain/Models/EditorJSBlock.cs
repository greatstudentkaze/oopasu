using System.Text.Json;

namespace StudyAssignmentManager.Domain
{
    public class EditorJSBlock
    {
       public string Type { get; set; }

       private string _data { get; set; }
       public object Data
       {
           get
           {
               return JsonSerializer.Deserialize<object>(_data);
           }
           set
           {
               _data = value.ToString();
           }
       }
    }
}