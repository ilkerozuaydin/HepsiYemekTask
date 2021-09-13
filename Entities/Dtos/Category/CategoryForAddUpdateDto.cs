using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.Category
{
    public class CategoryForAddUpdateDto : IDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}