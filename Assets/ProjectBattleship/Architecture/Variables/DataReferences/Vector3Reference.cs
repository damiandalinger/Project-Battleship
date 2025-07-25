/// <summary>
/// A serializable reference wrapper for a vector3 value, allowing selection between a constant value or a Vector3Variable.
/// Enables flexibility when designing systems in the Unity Inspector.
/// </summary>

/// <remarks>
/// Based on the ScriptableObject pattern by Ryan Hipple (Schell Games).
/// Extended and maintained by Damian Dalinger.
/// </remarks>

using UnityEngine;
using System;

namespace Dalinger.Architecture.Variables
{
    [Serializable]
    public class Vector3Reference : BaseReference<Vector3, Vector3Variable>
    {
        public Vector3Reference() : base() { }

        public Vector3Reference(Vector3 value) : base(value) { }

        protected override Vector3 GetValue()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }

        public static implicit operator Vector3(Vector3Reference reference)
        {
            return reference.Value;
        }
    }
}
