/// <summary>
/// Defines the properties of a ship type (e.g. name, length) used for placement and logic.
/// </summary>

/// <remarks>
/// Created by Damian Dalinger.
/// </remarks>

using UnityEngine;

[CreateAssetMenu(menuName = "Other/Ship Type")]
public class ShipTypeSO : ScriptableObject 
{
    [Tooltip("Length of the ship in tiles.")]
    public int length = 2;
}