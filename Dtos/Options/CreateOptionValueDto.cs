namespace clothes.api.Dtos.Options
{
    public class CreateOptionValueDto
    {
        public int OptionId { get; set; }
        public string Value { get; set; }
        public int Price { get; set; } = 0;
        public string? Thumbnail { get; set; }
    }
}
