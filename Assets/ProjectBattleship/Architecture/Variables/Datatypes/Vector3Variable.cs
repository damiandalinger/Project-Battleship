/// <summary>
/// A ScriptableObject-based container for a Vector3 value that allows runtime value modification.
/// Includes utility methods for setting, modifying and normalizing the value.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using UnityEngine;

namespace Dalinger.Architecture.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector3")]
    public class Vector3Variable : BaseVariable<Vector3>
    {
        #region Public Methods

        public void ApplyChange(Vector3 amount)
        {
            Value += amount;
        }

        public void ApplyChange(Vector3Variable amount)
        {
            Value += amount.Value;
        }

        public void Normalize()
        {
            Value = Value.normalized;
        }

        public void SetValue(Vector3 value)
        {
            Value = value;
        }

        public void SetValue(Vector3Variable value)
        {
            Value = value.Value;
        }

        #endregion
    }
}