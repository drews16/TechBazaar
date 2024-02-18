namespace TechBazaar.Domain.Entity
{
    public sealed class OrderProduct
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}