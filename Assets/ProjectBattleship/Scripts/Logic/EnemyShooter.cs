/// <summary>
/// Controls enemy AI shooting behavior.
/// Chooses a tile to shoot based on hit memory and logic to sink ships.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Dalinger.Architecture.Variables;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    #region Fields

    [Tooltip("Current turn state. Should be '2' for enemy turn.")]
    [SerializeField] private IntReference _turnState;

    [Tooltip("Reference to the player’s grid where the enemy shoots.")]
    [SerializeField] private GridManager _playerGrid;

    [Tooltip("Configuration of all tile states.")]
    [SerializeField] private TileStateConfig _tileStates;

    [Tooltip("Reference to the TurnManager to advance game turns.")]
    [SerializeField] private TurnManager _turnManager;

    private enum AIState { Searching, Targeting }
    private AIState _aiState = AIState.Searching;
    private List<Tile> _hitTiles = new();
    private Vector2Int[] _directions = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    private bool _isActing = false;

    #endregion

    #region Lifecycle Methods

    // Checks if it is the AI's turn and triggers the shooting coroutine.
    void Update()
    {
        if (_turnState.Value != 2 || _isActing) return;
        StartCoroutine(DelayedShoot());
    }

    #endregion

    #region Private Methods

    // Adds artificial delay before making a move to simulate decision making.
    private IEnumerator DelayedShoot()
    {
        _isActing = true;
        yield return new WaitForSeconds(Random.Range(0.4f, 0.9f)); // Denkzeit
        MakeAIMove();
        _isActing = false;
    }

    // Returns a random tile that has not been shot at yet.
    private Tile GetRandomUnrevealedTile()
    {
        List<Tile> candidates = new();
        for (int x = 0; x < _playerGrid.Width; x++)
        {
            for (int y = 0; y < _playerGrid.Height; y++)
            {
                Tile tile = _playerGrid.GetTile(x, y);
                if (!tile.IsShot)
                    candidates.Add(tile);
            }
        }

        return candidates.Count > 0 ? candidates[Random.Range(0, candidates.Count)] : null;
    }

    // Attempts to find the next best tile to shoot based on previous hits.
    private Tile GetNextTargetTile()
    {
        if (_hitTiles.Count < 2)
        {
            // Check adjacent tiles.
            foreach (var origin in _hitTiles)
            {
                foreach (var dir in _directions)
                {
                    Vector2Int pos = origin.GridPosition + dir;
                    Tile tile = _playerGrid.GetTile(pos.x, pos.y);
                    if (tile != null && !tile.IsShot)
                        return tile;
                }
            }
        }
        else
        {
            Vector2Int dir = _hitTiles[1].GridPosition - _hitTiles[0].GridPosition;

            Vector2Int forward = _hitTiles.Last().GridPosition;
            while (true)
            {
                forward += dir;
                Tile next = _playerGrid.GetTile(forward.x, forward.y);
                if (next == null || next.IsShot)
                    break;
                return next;
            }
            
            Vector2Int backward = _hitTiles[0].GridPosition;
            while (true)
            {
                backward -= dir;
                Tile next = _playerGrid.GetTile(backward.x, backward.y);
                if (next == null || next.IsShot)
                    break;
                return next;
            }
        }

        // Fallback
        return GetRandomUnrevealedTile();
    }

    // Performs the main logic to decide and shoot at a tile.
    private void MakeAIMove()
    {
        Tile target = (_aiState == AIState.Searching) ? GetRandomUnrevealedTile() : GetNextTargetTile();

        if (target == null)
        {
            Debug.LogWarning("No target found!");
            _turnManager.NextTurn(false);
            return;
        }

        bool hit = target.Shoot();

        if (hit)
        {
            _hitTiles.Add(target);

            if (target.GetShipInstance()?.IsSunk == true)
            {
                _hitTiles.Clear();
                _aiState = AIState.Searching;
            }
            else
            {
                _aiState = AIState.Targeting;
            }

            _turnManager.NextTurn(true);
        }
        else
        {
            _turnManager.NextTurn(false);
        }
    }

    #endregion
}
