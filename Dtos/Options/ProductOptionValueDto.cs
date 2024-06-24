using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Options
{
    public class ProductOptionValueDto
    {
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }
        public virtual Option Option { get; set; }
        public virtual OptionValue OptionValue { get; set; }
    }
}
