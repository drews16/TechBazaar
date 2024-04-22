namespace TechBazaar.Domain.Result
{
    public class BaseResult
    {
        public bool IsSuccess => ErrorMessage == null;
        public string? ErrorMessage { get; set; }
    }

    public class BaseResult<T>
    {
        public bool IsSuccess => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}