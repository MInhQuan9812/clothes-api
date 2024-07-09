using clothes.api.Dtos.Options;

namespace clothes.api.Dtos.Carts
{
    public class AddLineItemDto
    {
        //public int ProductId { get; set; }
        //public ICollection<OptionValueCartDto> Options { get;set; }=new List<OptionValueCartDto>();

        public int ProductVariantId { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsIncreasedBy { get; set; } = true;
    }
}
