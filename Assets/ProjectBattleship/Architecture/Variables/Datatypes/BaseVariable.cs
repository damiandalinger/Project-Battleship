/// <summary>
/// A generic ScriptableObject container for runtime variables of any type.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using System;
using UnityEngine;

namespace Dalinger.Architecture.Variables
{
    public abstract class BaseVariable<T> : ScriptableObject
    {
        #region Fields

#if UNITY_EDITOR
        [Multiline]
        [Tooltip("Editor-only description of what this variable is used for.")]
        public string DeveloperDescription = "";
#endif

        [Tooltip("The value of the variable.")]
        public T Value;

        #endregion
    }
}