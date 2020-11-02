namespace ConferenceDude.Domain.Exceptions
{
    using System;

    public class ModelException : Exception
    {
        public string PropertyName { get; }

        public ModelException(string message, string propertyName) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
