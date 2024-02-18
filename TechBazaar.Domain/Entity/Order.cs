using TechBazaar.Domain.Interfaces;

namespace TechBazaar.Domain.Entity
{
    public sealed class Order : IAuditable
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}