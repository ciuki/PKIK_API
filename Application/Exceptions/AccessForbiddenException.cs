using System.Globalization;

namespace Application.Exceptions
{
    public class AccessForbiddenException : Exception
    {
        public AccessForbiddenException()
        {
        }

        public AccessForbiddenException(string? message) : base(message)
        {
        }

        public AccessForbiddenException(string? message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
