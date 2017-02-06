namespace SuperZapatos.Exceptions
{
    using System;

    /// <summary>
    /// This exception raises when the record is not found or the results were 0.
    /// </summary>
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException()
        {
        }

        public RecordNotFoundException(string message) : base(message)
        {
        }

        public RecordNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}