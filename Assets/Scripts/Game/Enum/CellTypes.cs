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
        Unit_Abilities,

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