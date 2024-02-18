using TechBazaar.Domain.Interfaces;

namespace TechBazaar.Domain.Entity
{
    public sealed class Category : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SpecificationType> SpecificationTypes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}