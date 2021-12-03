namespace Game.Game
{
    public enum CellTypes
    {
        None,

        Cell,
        First = Cell,

        Unit,
        Unit_Stat,
        Unit_Effects,
        Unit_ToolWeapon,
        Unit_Shield,
        Unit_UniqueAbilities,
        //Unit_UniqueAbility,

        Build,
        Env,
        Trail,
        Fire,
        Cloud,
        River,

        Else,

        End
    }
}