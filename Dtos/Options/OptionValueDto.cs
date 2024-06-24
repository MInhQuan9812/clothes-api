using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Options
{
    public class OptionValueDto
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; }
        public string? Thumbnail { get;set; }
        public int Price { get; set; } = 0;
        //public virtual Option Option { get; set; }
        //public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; }

    }
}
