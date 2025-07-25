/// <summary>
/// Handles end-of-game conditions, displays results, and provides restart logic.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Dalinger.Architecture.Variables;

public class WinManager : MonoBehaviour
{
    #region Fields

    [Tooltip("Reference to the turn state variable.")]
    [SerializeField] private IntReference _turnState;

    [Tooltip("UI screen shown when the game ends.")]
    [SerializeField] private GameObject _endScreen;

    [Tooltip("Text field used to display the win/lose result.")]
    [SerializeField] private TMP_Text _resultText;

    [Tooltip("Tracks how many ships the player has remaining.")]
    [SerializeField] private IntReference _playerRemainingShips;

    [Tooltip("Tracks how many ships the enemy has remaining.")]
    [SerializeField] private IntReference _enemyRemainingShips;

    #endregion

    #region Public Methods

    // Checks if either the player or enemy has won and triggers the result screen.
    public void CheckWinCondition()
    {
        if (_playerRemainingShips.Value == 0)
        {
            ShowResult(false);
        }
        if (_enemyRemainingShips.Value == 0)
        {
            ShowResult(true);
        }
    }

    // Shows the end screen with win/lose message and sets the game to idle state.
    public void ShowResult(bool playerWon)
    {
        _turnState.Variable.SetValue(3);
        _endScreen.SetActive(true);
        _resultText.text = playerWon ? "YOU WIN!" : "YOU LOSE";
    }

    // Restarts the current scene, effectively restarting the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
