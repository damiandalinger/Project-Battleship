/// <summary>
/// Represents an individual ship instance during gameplay. 
/// Tracks which tiles the ship occupies and whether it has been sunk.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using System.Collections.Generic;
using System.Linq;
using System;

public class ShipInstance
{
    #region Fields
    public event Action OnSunk;
    private readonly List<Tile> _tiles;
    private bool _alreadySunk = false;

    #endregion

    #region Properties

    // Returns true if all tiles of the ship have been shot.
    public bool IsSunk => _tiles.All(t => t.IsShot);

    #endregion

    #region Constructor

    // Initializes a new ship instance with a list of occupied tiles.
    public ShipInstance(List<Tile> shipTiles)
    {
        _tiles = shipTiles;
    }

    #endregion

    #region Public Methods

    // Marks the ship as sunk and changes its tiles to the given sunk state.
    public void MarkAsSunk(TileState sunkState)
    {
        if (_alreadySunk)
            return;

        _alreadySunk = true;

        foreach (var tile in _tiles)
        {
            tile.SetState(sunkState);
        }

        OnSunk?.Invoke();
    }

    #endregion
}