namespace TechBazaar.Domain.Dto.Order
{
    public sealed class CreateOrderDto 
    {
        public Guid UserId { get; set; }
        public required int OfficeId { get; init; }
        public required int CartId { get; init; }
        public required decimal TotalPrice { get; init; }
    }
}