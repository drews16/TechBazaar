namespace TechBazaar.Domain.Entity
{
    public sealed class Specification
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int SpecificationTypeId { get; set; }
        public SpecificationType SpecificationType { get; set; } 
        public string Value { get; set; }
    }
}