using TechBazaar.Domain.Interfaces;

namespace TechBazaar.Domain.Entity
{
    public sealed class Brand : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}