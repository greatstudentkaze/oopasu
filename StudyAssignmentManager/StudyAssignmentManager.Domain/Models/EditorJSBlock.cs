namespace StudyAssignmentManager.Domain
{
    public class EditorJSBlock
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public BlockData Data { get; set; }
    }
    
    public class BlockData {
        public string Text { get; set; }
        // Header
        public int? Level { get; set; }
        // List
        public string[]? Items { get; set; }
        public string? Style { get; set; }
    }
}