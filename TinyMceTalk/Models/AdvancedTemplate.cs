namespace TinyMceTalk.Models
{
    public class AdvancedTemplateCategoryResponse
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public IEnumerable<AdvancedTemplateResponse>? Items { get; set; }
    }

    public class AdvancedTemplateResponse
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }
    }    
}
