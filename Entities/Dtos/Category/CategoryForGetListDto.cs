using Core.Entities.Abstract;

namespace Entities.Dtos.Category
{
    public class CategoryForGetListDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}