/// <summary>
/// Custom Inspector for GameEvents that adds a runtime-only "Raise" button for testing.
/// </summary>

/// <remarks>
/// Created and maintained by Damian Dalinger.
/// </remarks>

using UnityEditor;
using UnityEngine;

namespace Dalinger.Architecture.Events
{
    [CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
    public class EventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}
