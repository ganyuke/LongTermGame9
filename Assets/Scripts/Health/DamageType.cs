[System.Flags]
public enum DamageType
{
    PlayerKick = 1 << 0,
    PlayerTailWhip = 1 << 1,
    PlayerThrow = 1 << 2,
    PlayerStomp = 1 << 3,
    Cactus = 1 << 4,
    Enemy = 1 << 5,
}
