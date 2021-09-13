
using Core.Entities.Abstract;

namespace Entities.Dtos.Category
{
    public class CategoryResponseModel:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
