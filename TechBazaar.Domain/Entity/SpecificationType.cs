namespace TechBazaar.Domain.Entity
{
    public sealed class SpecificationType
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Specification> Specifications { get; set; }
        public string  SpecificationName { get; set; }
        public string DisplayName { get; set; }
        public string EngUnit { get; set; }
    }
}