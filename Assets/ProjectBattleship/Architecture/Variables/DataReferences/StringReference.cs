/// <summary>
/// A serializable reference wrapper for a string value, allowing selection between a constant value or a StringVariable.
/// Enables flexibility when designing systems in the Unity Inspector.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using System;

namespace Dalinger.Architecture.Variables
{
    [Serializable]
    public class StringReference : BaseReference<string, StringVariable>
    {
        public StringReference() : base() { }

        public StringReference(string value) : base(value) { }

        protected override string GetValue()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }

        public static implicit operator string(StringReference reference)
        {
            return reference.Value;
        }
    }
}
