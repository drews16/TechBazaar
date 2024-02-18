namespace TechBazaar.Domain.Entity
{
    public sealed class Cart
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<CartProduct> CartProducts { get; set; }
    }
}