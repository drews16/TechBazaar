using TechBazaar.Domain.Interfaces;

namespace TechBazaar.Domain.Entity
{
    public sealed class User : IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        public UserToken UserToken { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}