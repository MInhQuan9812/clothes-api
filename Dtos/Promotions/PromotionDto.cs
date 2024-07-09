namespace clothes.api.Dtos.Promotions
{
    public class PromotionDto
    {
        public string Name { get; set; }
        public int PromotionTypeId { get; set; }
        public int PromotionValue { get; set; }
        public virtual PromotionTypeDto PromotionType { get;set;}
    }
}
