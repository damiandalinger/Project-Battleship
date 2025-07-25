/// <summary>
/// Handles interactive placement of player ships on the grid via mouse input.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using System.Collections.Generic;
using UnityEngine;
using Dalinger.Architecture.Events;
using Unity.VisualScripting;

public class PlayerShipPlacer : MonoBehaviour
{
    #region Fields

    [Tooltip("List of ships the player needs to place on their grid.")]
    [SerializeField] private List<ShipTypeSO> _shipsToPlace;

    [Tooltip("Main camera used to cast ray from mouse position.")]
    [SerializeField] private Camera _camera;

    [Tooltip("Tile state visuals for previews and placements.")]
    [SerializeField] private TileStateConfig _config;

    [Tooltip("Event raised once all ships have been placed.")]
    [SerializeField] private GameEvent _onShipsPlaced;

    [Tooltip("Reference to the ShipManager responsible for placement logic.")]
    [SerializeField] private ShipManager _shipManager;

    [Tooltip("Reference to the player's grid.")]
    [SerializeField] private GridManager _gridManager;

    private int currentIndex = 0;
    private bool isVertical = false;

    #endregion

    #region Lifecycle Methods

    // Handles user input for placing ships and toggling orientation.
    void Update()
    {
        if (currentIndex >= _shipsToPlace.Count) return;

        if (Input.GetKeyDown(KeyCode.Q))
            isVertical = !isVertical;

        if (!GetTileUnderMouse(out Tile hoveredTile)) return;

        ClearPreview();

        var ship = _shipsToPlace[currentIndex];
        var validTiles = _shipManager.GetValidTiles(_gridManager, hoveredTile.GridPosition, ship.length, isVertical);

        if (validTiles != null)
        {
            PreviewTiles(validTiles, _config.ValidPreview);

            if (Input.GetMouseButtonDown(0))
            {
                var result = _shipManager.TryPlaceShip(hoveredTile.GridPosition, ship.length, isVertical, _config.Placed, isPlayer: true);
                if (result != null)
                {
                    currentIndex++;
                    if (currentIndex >= _shipsToPlace.Count)
                        _onShipsPlaced?.Raise();
                }
            }
        }
        else
        {
            var fallbackTiles = GetTilesEvenIfInvalid(hoveredTile.GridPosition, ship.length, isVertical);
            PreviewTiles(fallbackTiles, _config.InvalidPreview);
        }
    }

    #endregion

    #region Private Methods

    // Highlights the given tiles with a specific state (valid or invalid preview).
    private void PreviewTiles(List<Tile> tiles, TileState state)
    {
        foreach (var tile in tiles)
        {
            if (!tile.IsOccupied)
                tile.SetState(state, isPreview: true);
        }
    }

    // Clears all preview visuals from the grid.
    private void ClearPreview()
    {
        for (int x = 0; x < _gridManager.Width; x++)
        {
            for (int y = 0; y < _gridManager.Height; y++)
            {
                _gridManager.GetTile(x, y)?.RevertPreview();
            }
        }
    }

    // Returns a list of tiles at the desired placement position, even if the placement is invalid.
    private List<Tile> GetTilesEvenIfInvalid(Vector2Int pos, int length, bool vertical)
    {
        List<Tile> tiles = new();
        for (int i = 0; i < length; i++)
        {
            int x = pos.x + (vertical ? 0 : i);
            int y = pos.y + (vertical ? i : 0);
            var tile = _gridManager.GetTile(x, y);
            if (tile != null) tiles.Add(tile);
        }
        return tiles;
    }

    // Casts a ray from the mouse cursor and tries to identify a tile under the pointer.
    private bool GetTileUnderMouse(out Tile tile)
    {
        tile = null;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            tile = hit.collider.GetComponent<Tile>();
            return tile != null && tile.GridOwner == _gridManager;
        }
        return false;
    }

    #endregion
}
