namespace SuperZapatos.Exceptions
{
    using System;

    /// <summary>
    /// This exception raises when the data is no correct, ex: A guid is not a guid.
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}