/// <summary>
/// A serializable reference wrapper for a bool value, allowing selection between a constant value or a BoolVariable.
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
    public class BoolReference : BaseReference<bool, BoolVariable>
    {
        public BoolReference() : base() { }

        public BoolReference(bool value) : base(value) { }

        protected override bool GetValue()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }

        public static implicit operator bool(BoolReference reference)
        {
            return reference.Value;
        }
    }
}
