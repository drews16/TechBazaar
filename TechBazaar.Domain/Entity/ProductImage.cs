namespace TechBazaar.Domain.Entity
{
    public sealed class ProductImage
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public string Path { get; set; }
    }
}