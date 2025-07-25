/// <summary>
/// A ScriptableObject-based container for a string value that allows runtime value modification.
/// Includes utility methods for setting, appending and clearing the value.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using UnityEngine;

namespace Dalinger.Architecture.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : BaseVariable<string>
    {
        #region Public Methods

        public void Append(string extra)
        {
            Value += extra;
        }

        public void Append(StringVariable extra)
        {
            Value += extra;
        }

        public void Clear()
        {
            Value = "";
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void SetValue(StringVariable value)
        {
            Value = value.Value;
        }

        #endregion
    }
}