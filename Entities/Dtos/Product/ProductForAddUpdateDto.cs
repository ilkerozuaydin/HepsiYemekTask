using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos.Product
{
    public class ProductForAddUpdateDto : IDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}