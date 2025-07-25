/// <summary>
/// Central configuration asset that defines all tile states used during gameplay.
/// Contains references to visual states such as hit, miss, preview, and placed.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using UnityEngine;

[CreateAssetMenu(menuName = "Other/Tile State Config")]
public class TileStateConfig : ScriptableObject
{
    public TileState Empty;
    public TileState ValidPreview;
    public TileState InvalidPreview;
    public TileState Placed;
    public TileState PlacedHidden;
    public TileState Hit;
    public TileState Miss;
    public TileState Sunk;
}