/// <summary>
/// Represents a single tile on the grid. Handles visual state, interaction, and ship assignment.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{
    #region Fields

    [Tooltip("Defines the possible visual and logical states of this tile.")]
    [SerializeField] private TileStateConfig _config;

    private MeshRenderer _meshRenderer;
    private TileState _currentState;
    private TileState _savedStateBeforePreview;
    private ShipInstance _shipInstance;

    #endregion

    #region Properties

    public Vector2Int GridPosition { get; set; }
    public GridManager GridOwner { get; set; }
    public bool IsOccupied => _currentState == _config.Placed || _currentState == _config.PlacedHidden;
    public bool IsPreviewTemporary { get; private set; } = false;
    public bool IsShot { get; private set; } = false;

    #endregion

    #region Lifecycle Methods

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        SetState(_config.Empty);
    }

    #endregion

    #region Public Methods

    // Assigns a ship to this tile.
    public void SetShipInstance(ShipInstance ship)
    {
        _shipInstance = ship;
    }

    // Returns the ship instance occupying this tile, or null.
    public ShipInstance GetShipInstance()
    {
        return _shipInstance;
    }

    // Applies a new tile state and updates visuals. Optionally marks as a preview.
    public void SetState(TileState newState, bool isPreview = false)
    {
        if (isPreview && !IsPreviewTemporary)
            _savedStateBeforePreview = _currentState;

        _currentState = newState;
        IsPreviewTemporary = isPreview;
        UpdateVisual();
    }

    // Reverts the tile to its state before the preview was applied.
    public void RevertPreview()
    {
        if (IsPreviewTemporary)
        {
            _currentState = _savedStateBeforePreview;
            IsPreviewTemporary = false;
            UpdateVisual();
        }
    }

    // Shoots this tile. Returns true if it was a hit, false if a miss or already shot.
    public bool Shoot()
    {
        if (IsShot)
            return false;

        IsShot = true;

        if (IsOccupied)
        {
            SetState(_config.Hit);

            if (_shipInstance != null && _shipInstance.IsSunk)
            {
                _shipInstance.MarkAsSunk(_config.Sunk);
            }

            return true;
        }
        else
        {
            SetState(_config.Miss);
            return false;
        }
    }

    #endregion

    #region Private Methods

    // Applies the current state's color to the tile's material.
    private void UpdateVisual()
    {
        if (_currentState != null)
            _meshRenderer.material.color = _currentState.TileColor;
    }

    #endregion
}