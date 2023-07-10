#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace ArcardeWaveShooter.Exceptions
{
    public class SerializeFieldNotAssignedException : Exception
    {
        public static void ThrowIfNull([NotNull] object? objectToTest, string objectName)
        {
            if (objectToTest == null)
            {
                throw new($"SerializeField {objectName} was not assigned in the inspector.");
            }
        }
    }
}
