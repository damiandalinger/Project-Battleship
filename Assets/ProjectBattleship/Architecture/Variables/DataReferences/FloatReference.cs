/// <summary>
/// A serializable reference wrapper for a float value, allowing selection between a constant value or a FloatVariable.
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
    public class FloatReference : BaseReference<float, FloatVariable>
    {
        public FloatReference() : base() { }

        public FloatReference(float value) : base(value) { }

        protected override float GetValue()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}
