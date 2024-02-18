namespace TechBazaar.Domain.Entity
{
    public sealed class UserToken
    {
        public long Id { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}