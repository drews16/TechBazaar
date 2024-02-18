namespace TechBazaar.Domain.Entity
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}