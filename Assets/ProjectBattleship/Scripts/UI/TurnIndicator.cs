/// <summary>
/// Displays current turn state in the UI using a TMP text field.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using UnityEngine;
using TMPro;
using Dalinger.Architecture.Variables;

public class TurnIndicator : MonoBehaviour
{
    [Tooltip("Reference to the current turn state variable (0 = Setup, 1 = Player, 2 = Enemy, 3 = Idle).")]
    [SerializeField] private IntReference _turnState;

    [Tooltip("UI text field that displays the current turn information.")]
    [SerializeField] private TMP_Text _turnText;

    void Update()
    {
        switch (_turnState.Value)
        {
            case 0:
                _turnText.text = "Place your Ships"; 
                break;
            case 1:
                _turnText.text = "Your Turn";
                break;
            case 2:
                _turnText.text = "Enemy Turn";
                break;
            case 3:
                _turnText.text = "";
                break;
            default:
                _turnText.text = "";
                break;
        }
    }
}
