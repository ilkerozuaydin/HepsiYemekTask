using Core.Entities.Abstract;
using Entities.Dtos.Category;

namespace Entities.Dtos.Product
{
   public class ProductResponseModel:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryResponseModel Category { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
