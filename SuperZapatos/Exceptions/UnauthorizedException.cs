namespace SuperZapatos.Exceptions
{
    using System;

    /// <summary>
    /// This exception raises when the user does not have the premision to access de Web Api.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}