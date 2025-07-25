/// <summary>
/// Automatically places enemy ships randomly on the grid during game setup.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using System.Collections.Generic;
using UnityEngine;

public class EnemyShipPlacer : MonoBehaviour
{
    #region Fields

    [Tooltip("List of enemy ship types that should be placed on the grid.")]
    [SerializeField] private List<ShipTypeSO> _shipsToPlace;

    [Tooltip("Tile state configuration used to define different states.")]
    [SerializeField] private TileStateConfig _config;

    [Tooltip("Toggle to show placed ships for debugging purposes.")]
    [SerializeField] private bool _showDebugShips = false;

    [Tooltip("Reference to the ShipManager used to place and track ships.")]
    [SerializeField] private ShipManager _shipManager;

    [Tooltip("Reference to the GridManager used to query grid dimensions.")]
    [SerializeField] private GridManager _gridManager;

    #endregion

    #region Public Methods

    // Places all enemy ships randomly onto the grid.
    public void PlaceShips()
    {
        foreach (var ship in _shipsToPlace)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < 100)
            {
                bool vertical = Random.value > 0.5f;
                Vector2Int pos = new(
                    Random.Range(0, _gridManager.Width),
                    Random.Range(0, _gridManager.Height)
                );
                TileState stateToUse = _showDebugShips ? _config.Placed : _config.PlacedHidden;
                var result = _shipManager.TryPlaceShip(pos, ship.length, vertical, stateToUse, isPlayer: false);
                if (result != null) placed = true;
                attempts++;
            }
        }
    }

    #endregion
}
