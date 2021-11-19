namespace Game.Game
{
    public enum CellClickTypes
    {
        Start,
        None = Start,

        SelCell,
        First = SelCell,

        SetUnit,
        Second = SetUnit,

        GiveTakeTW,
        Third = GiveTakeTW,


        UpgradeUnit,
        GiveScout,
        GiveHero,
        UniqAbil,

        End,
    }
}
