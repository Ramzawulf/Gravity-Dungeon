using System;

namespace Assets.Scripts.Tile
{
    [Serializable]
    public enum TileType {
        Empty,
        Switch,
        Slippery,
        Catapult,
        Spike,
        Fire,
        Transporter,
        Obstacle
    }
}
