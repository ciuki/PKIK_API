using System.Globalization;

namespace Application.Exceptions
{
    public class ObjectValidationException : Exception
    {
        public ObjectValidationException()
        {
        }

        public ObjectValidationException(string? message) : base(message)
        {
        }

        public ObjectValidationException(string? message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
