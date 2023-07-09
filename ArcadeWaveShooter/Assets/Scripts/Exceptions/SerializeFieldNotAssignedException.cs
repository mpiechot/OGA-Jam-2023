using System;

namespace Assets.Scripts.Exceptions
{
    public class SerializeFieldNotAssignedException : Exception
    {
        public static void ThrowIfNull(object? objectToTest, string objectName)
        {
            if (objectToTest == null)
            {
                throw new($"SerializeField {objectName} was not assigned in the inspector.");
            }
        }
    }
}
