﻿using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Options
{
    public class OptionValueDto
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; } = 0;
        public string Thumbnail {  get; set; }
        //public virtual Option Option { get; set; }
        //public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; }

    }
}
