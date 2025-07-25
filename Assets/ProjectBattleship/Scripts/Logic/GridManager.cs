/// <summary>
/// Generates and manages a grid of tiles used for ship placement and gameplay.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using Dalinger.Architecture.Variables;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Fields

    public int Width;
    public int Height;

    [Tooltip("Prefab used to instantiate individual tiles.")]
    [SerializeField] private GameObject _tilePrefab;

    [Tooltip("World offset applied to the grid origin position.")]
    [SerializeField] private Vector3 _originOffset;

    [Tooltip("Spacing between each tile in world units.")]
    [SerializeField] private FloatReference _spacing;

    private Tile[,] grid;

    #endregion

    #region Lifecycle Methods

    void Start()
    {
        GenerateGrid();
    }

    #endregion

    #region Public Methods

    // Returns the tile at the given grid coordinates, or null if out of bounds.
    public Tile GetTile(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return grid[x, y];
        }
        return null;
    }

    #endregion

    #region Private Methods

    // Generates a grid of tiles using the specified width, height, and spacing.
    private void GenerateGrid()
    {
        grid = new Tile[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3 position = new Vector3(x * _spacing.Value, 0, y * _spacing.Value) + _originOffset;
                GameObject tileGO = Instantiate(_tilePrefab, position, Quaternion.identity, transform);

                Tile tile = tileGO.GetComponent<Tile>();
                tile.GridPosition = new Vector2Int(x, y);
                tile.GridOwner = this;

                grid[x, y] = tile;
            }
        }
    }

    #endregion
}