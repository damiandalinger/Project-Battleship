/// <summary>
/// A serializable reference wrapper for a int value, allowing selection between a constant value or a IntVariable.
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
    public class IntReference : BaseReference<int, IntVariable>
    {
        public IntReference() : base() { }

        public IntReference(int value) : base(value) { }

        protected override int GetValue()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }

        public static implicit operator int(IntReference reference)
        {
            return reference.Value;
        }
    }
}
