namespace TechBazaar.Domain.Entity
{
    public sealed class Office
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}