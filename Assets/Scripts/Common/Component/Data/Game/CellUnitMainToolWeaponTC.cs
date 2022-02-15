namespace Game.Game
{
    public sealed class CellUnitMainToolWeaponTC : ToolWeaponTC, IIsMeleeCellUnit
    {
        public bool IsMelee => !Is(ToolWeaponTypes.BowCrossbow);
    }
}