using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models.Entities
{
    [Index(nameof(Sku), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Key { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(50)]
        public string Sku { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        [Precision(16, 2)]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        [MaxLength(15)]
        public string ImageType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
