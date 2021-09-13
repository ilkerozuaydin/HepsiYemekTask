using Core.Entities.Abstract;

namespace Entities.Dtos.Product
{
    public class ProductForGetListDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}