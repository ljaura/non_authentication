
using System.Net;

namespace UserService.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class DomainException : Exception
    {
        public int StatusCode { get; }

        public DomainException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = (int)statusCode;
        }

        public DomainException(HttpStatusCode statusCode, string message, Exception exception)
            : base(message, exception)
        {
            StatusCode = (int)statusCode;
        }
    }
}
