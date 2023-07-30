using System;
using System.Runtime.Serialization;

[Serializable]
internal class ObjectNotInitializedException : Exception
{
    public ObjectNotInitializedException()
    {
    }

    public ObjectNotInitializedException(string message) : base(message)
    {
    }

    public ObjectNotInitializedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ObjectNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}