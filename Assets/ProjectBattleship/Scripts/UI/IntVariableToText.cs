/// <summary>
// Displays the current value of an IntReference in a UI Text field with optional suffix.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using Dalinger.Architecture.Variables;
using TMPro;
using UnityEngine;

public class IntVariableToText : MonoBehaviour
{
    [Tooltip("The TMP_Text component to display the variable value.")]
    [SerializeField] private TMP_Text _textField;

    [Tooltip("The integer variable whose value should be displayed.")]
    [SerializeField] private IntReference _variable;

    [Tooltip("Optional string to append after the number (e.g. %, pts, etc.).")]
    [SerializeField] private string _suffix = "";

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        _textField.text = _variable.Value.ToString() + _suffix;
    }
}
