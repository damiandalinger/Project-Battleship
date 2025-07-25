/// <summary>
/// A ScriptableObject-based container for a bool value that allows runtime value modification.
/// Includes utility methods for setting and toggling the value.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using UnityEngine;

namespace Dalinger.Architecture.Variables
{
    [CreateAssetMenu(menuName = "Variables/Bool")]
    public class BoolVariable : BaseVariable<bool>
    {
        #region Public Methods

        public void SetValue(bool value)
        {
            Value = value;
        }

        public void SetValue(BoolVariable value)
        {
            Value = value.Value;
        }

        public void Toggle()
        {
            Value = !Value;
        }

        #endregion
    }
}