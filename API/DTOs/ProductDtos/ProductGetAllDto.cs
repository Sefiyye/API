using System.Collections.Generic;

namespace API.DTOs.ProductDtos
{
    public class ProductGetAllDto
    {
        public List<ProductListItemDto> Products { get; set; }
        public int TotalCount { get; set; }

        public ProductGetAllDto()
        {
            Products = new();
        }
    }
}
