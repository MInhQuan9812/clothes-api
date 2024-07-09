using clothes.api.Dtos.Carts;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Products
{
    public class ProductSkuDto
    {
        public int Id { get; set; }
        public string VariantName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<VariantValueDto> VariantValues { get; set; } = new List<VariantValueDto>();

    }
}
