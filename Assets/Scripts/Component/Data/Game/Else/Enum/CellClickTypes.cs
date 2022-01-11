namespace Game.Game
{
    public enum CellClickTypes
    {
        None,
        Start = None,

        SimpleClick,
        First = SimpleClick,

        SetUnit,
        GiveTakeTW,
        UpgradeUnit,
        GiveScout,
        GiveHero,
        UniqAbil,

        //FirstClick,
        //AllOtherClicks,

        End,
    }
}
