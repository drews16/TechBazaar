using TechBazaar.Domain.Interfaces;

namespace TechBazaar.Domain.Entity
{
    public sealed class Product : IAuditable
    {
        public long Id { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Model { get; set; }
        public decimal  Price { get; set; }
        public string MainImage { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Specification> Specifications { get; set; }
        public int CountPurchase { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}