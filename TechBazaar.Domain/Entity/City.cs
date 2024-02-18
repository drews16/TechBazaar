namespace TechBazaar.Domain.Entity
{
    public sealed class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Office> Offices { get; set; }
    }
}