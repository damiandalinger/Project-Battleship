/// <summary>
/// Manages the current turn state of the game and transitions between player and enemy turns.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using Dalinger.Architecture.Variables;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Tooltip("Reference to the turn state variable (0 = Setup, 1 = Player, 2 = Enemy).")]
    [SerializeField] private IntReference _turnState;

    [Tooltip("Reference to the player's grid.")]
    [SerializeField] private GridManager _playerGrid;

    [Tooltip("Reference to the enemy's grid.")]
    [SerializeField] private GridManager _enemyGrid;

    void Start()
    {
        _turnState.Variable.SetValue(0);
    }
    public void StartTurnPhase()
    {
        _turnState.Variable.SetValue(1);
    }

    // Advances to the next turn if no hit occurred. Alternates between player and enemy.
    public void NextTurn(bool wasHit)
    {
        if (_turnState.Value != 1 && _turnState.Value != 2) return; 

        if (!wasHit)
        {
            int next = (_turnState.Value == 1) ? 2 : 1;
            _turnState.Variable.SetValue(next);
        }
    }
}