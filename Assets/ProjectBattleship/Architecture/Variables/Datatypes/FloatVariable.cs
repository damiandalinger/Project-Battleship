/// <summary>
/// A ScriptableObject-based container for a float value that allows runtime value modification.
/// Includes utility methods for setting, modifying, multiplying, dividing, and clamping the value.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using UnityEngine;

namespace Dalinger.Architecture.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    public class FloatVariable : BaseVariable<float>
    {
        #region Public Methods

        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }

        public void Clamp(float min, float max)
        {
            Value = Mathf.Clamp(Value, min, max);
        }

        public void Clamp(FloatVariable min, float max)
        {
            Value = Mathf.Clamp(Value, min.Value, max);
        }

        public void Clamp(float min, FloatVariable max)
        {
            Value = Mathf.Clamp(Value, min, max.Value);
        }

        public void Clamp(FloatVariable min, FloatVariable max)
        {
            Value = Mathf.Clamp(Value, min.Value, max.Value);
        }

        public void Divide(float divisor)
        {
            Value /= divisor;
        }

        public void Divide(FloatVariable divisor)
        {
            Value /= divisor.Value;
        }

        public void Multiply(float factor)
        {
            Value *= factor;
        }

        public void Multiply(FloatVariable factor)
        {
            Value *= factor.Value;
        }

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        #endregion
    }
}
