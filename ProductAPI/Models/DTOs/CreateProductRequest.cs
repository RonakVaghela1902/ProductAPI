using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models.DTOs
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Sku { get; set; } = string.Empty;
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageBase64 { get; set; } = string.Empty;
        public string ImageType { get; set; } = string.Empty;
    }
}
