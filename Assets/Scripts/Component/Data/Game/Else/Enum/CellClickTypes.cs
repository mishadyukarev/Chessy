namespace Game.Game
{
    public enum CellClickTypes
    {
        None,
        Start = None,

        SimpleClick,

        SetUnit,
        GiveTakeTW,
        UpgradeUnit,
        GiveScout,
        GiveHero,
        UniqueAbility,

        //FirstClick,
        //AllOtherClicks,

        End,
    }
}
