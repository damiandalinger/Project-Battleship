/// <summary>
/// Handles ship placement, validation, and tracking for both player and enemy grids.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using System.Collections.Generic;
using UnityEngine;
using Dalinger.Architecture.Variables;
using Dalinger.Architecture.Events;

public class ShipManager : MonoBehaviour
{
    #region Fields

    [Header("Setup")]

    [Tooltip("Reference to the player's grid.")]
    [SerializeField] private GridManager _playerGrid;

    [Tooltip("Reference to the enemy's grid.")]
    [SerializeField] private GridManager _enemyGrid;

    [Tooltip("Event raised when a ship is sunk.")]
    [SerializeField] private GameEvent _onShipSunk;

    [Tooltip("Configuration for tile visual states.")]
    [SerializeField] private TileStateConfig _tileStateConfig;

    [Header("Tracking")]

    [Tooltip("Variable that tracks remaining player ships.")]
    [SerializeField] private IntReference _playerRemainingShips;

    [Tooltip("Variable that tracks remaining enemy ships.")]
    [SerializeField] private IntReference _enemyRemainingShips;

    private List<ShipInstance> playerShips = new();
    private List<ShipInstance> enemyShips = new();

    #endregion

    #region Lifecycle Methods

    // Initializes ship counters when enabled.
    private void OnEnable()
    {
        _playerRemainingShips.Variable.SetValue(0);
        _enemyRemainingShips.Variable.SetValue(0);
    }

    #endregion

    #region Public Methods

    // Tries to place a ship at the given position on the specified grid.
    public ShipInstance TryPlaceShip(Vector2Int startPos, int length, bool vertical, TileState state, bool isPlayer)
    {
        var grid = isPlayer ? _playerGrid : _enemyGrid;
        var tiles = GetValidTiles(grid, startPos, length, vertical);
        if (tiles == null) return null;

        ShipInstance ship = new(tiles);

        foreach (var tile in tiles)
        {
            tile.SetState(state);
            tile.SetShipInstance(ship);
        }

        RegisterShip(ship, isPlayer);
        return ship;
    }

    // Validates whether a ship can be placed at a given position and returns the tile list.
    public List<Tile> GetValidTiles(GridManager grid, Vector2Int startPos, int length, bool vertical)
    {
        List<Tile> tiles = new();
        for (int i = 0; i < length; i++)
        {
            int x = startPos.x + (vertical ? 0 : i);
            int y = startPos.y + (vertical ? i : 0);

            var tile = grid.GetTile(x, y);
            if (tile == null || tile.IsOccupied)
                return null;

            tiles.Add(tile);
        }
        return tiles;
    }

    #endregion

    #region Private Methods

    // Registers a ship and hooks into the OnSunk event to track remaining ship counts.
    private void RegisterShip(ShipInstance ship, bool isPlayer)
    {
        if (isPlayer)
        {
            playerShips.Add(ship);
            _playerRemainingShips.Variable.SetValue(playerShips.Count);
        }
        else
        {
            enemyShips.Add(ship);
            _enemyRemainingShips.Variable.SetValue(enemyShips.Count);
        }

        ship.OnSunk += () =>
        {
            if (isPlayer)
                _playerRemainingShips.Variable.ApplyChange(-1);
            else
                _enemyRemainingShips.Variable.ApplyChange(-1);

            _onShipSunk?.Raise();
        };
    }

    #endregion
}
