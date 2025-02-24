/// <summary>
/// Defines how a tile receives damage.
/// </summary>
public enum TileDamageType
{
    /// <summary>
    /// The tile is indestructible and does not receive damage.
    /// </summary>
    None,

    /// <summary>
    /// The tile receives damage directly from a click, or from clicking a neighboring tile of the same type.
    /// </summary>
    Click,

    /// <summary>
    /// The tile receives damage when in proximity to a tile that has been clicked.
    /// </summary>
    Proximity,
}
