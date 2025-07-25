/// <summary>
/// Handles player shooting input on the enemy grid during their turn.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using Dalinger.Architecture.Variables;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    #region Fields

    [Tooltip("Reference to the current turn state (1 = player turn).")]
    [SerializeField] private IntReference _turnState;

    [Tooltip("Main camera used for raycasting the mouse position.")]
    [SerializeField] private Camera _camera;

    [Tooltip("Reference to the enemy grid to target tiles.")]
    [SerializeField] private GridManager _enemyGrid;

    [Tooltip("Tile state configuration for hit and miss visuals.")]
    [SerializeField] private TileStateConfig _tileStates;

    [Tooltip("Manages the current turn and transitions between players.")]
    [SerializeField] private TurnManager _turnManager;

    #endregion

    #region Lifecycle Methods

    // Handles player input and shooting logic when it's the player's turn.
    void Update()
    {
        if (_turnState.Value != 1) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (GetTileUnderMouse(out Tile tile) && tile.GridOwner == _enemyGrid)
            {
                if (tile.IsShot)
                    return;

                bool hit = tile.Shoot();

                _turnManager.NextTurn(hit);
            }
        }
    }

    #endregion

    #region Private Methods

    // Casts a ray from the mouse to find a tile under the cursor.
    private bool GetTileUnderMouse(out Tile tile)
    {
        tile = null;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            tile = hit.collider.GetComponent<Tile>();
            return tile != null;
        }
        return false;
    }

    #endregion
}
