using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models.DTOs;
using ProductAPI.Models.Entities;
using System.Text.RegularExpressions;

namespace ProductAPI.Repositories
{
    public class ProductRepo: IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepo> _logger;
        private readonly IWebHostEnvironment _env;

        public ProductRepo(ApplicationDbContext context, ILogger<ProductRepo> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        public async Task<Product> CreateProduct(CreateProductRequest request)
        {
            byte[] imageBytes = null;
            string fileName = null;
            string sku;
            
            if (!string.IsNullOrEmpty(request.Sku))
            {
                request.Sku = request.Sku.ToUpper();
                if (!Regex.IsMatch(request.Sku, @"^PRD-\d{4}$"))
                    throw new Exception("Invalid SKU format. Expected PRD-1234");

                bool isExists = await _context.Products.AnyAsync(p => p.Sku == request.Sku);
                if (isExists)
                    throw new Exception("SKU already exists");
            }

            if (!string.IsNullOrEmpty(request.ImageBase64))
            {
                try
                {
                    imageBytes = Convert.FromBase64String(request.ImageBase64);

                    if (imageBytes.Length > 2 * 1024 * 1024)
                        throw new Exception("Image too large");

                    fileName = Guid.NewGuid() + "." + request.ImageType;
                    string folderPath = Path.Combine(_env.WebRootPath, "images");
                    Directory.CreateDirectory(folderPath);

                    string path = Path.Combine(folderPath, fileName);

                    await File.WriteAllBytesAsync(path, imageBytes);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Image Processing Failed");
                    throw;
                }
                
            }

            Product product = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Category = request.Category,
                Price = request.Price,
                Description = request.Description,
                ImagePath = fileName,
                ImageType = request.ImageType
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
