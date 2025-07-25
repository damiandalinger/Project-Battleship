/// <summary>
/// Represents a visual state of a tile, defined by its color.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using UnityEngine;

[CreateAssetMenu(menuName = "Other/Tile State")]
public class TileState : ScriptableObject
{
    public Color TileColor;
}