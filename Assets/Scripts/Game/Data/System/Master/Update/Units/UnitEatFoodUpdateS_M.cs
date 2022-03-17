namespace Chessy.Game.System.Model
{
    static class UnitEatFoodUpdateS_M
    {
        public static void Run(in SystemsModelManager sMM, in EntitiesModel e)
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (e.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    e.PlayerInfoE(player).ResourcesC(res).Resources = 0;

                    for (var unitT = UnitTypes.Elfemale; unitT >= UnitTypes.Pawn; unitT--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            for (byte idx_0 = 0; idx_0 < e.LengthCells; idx_0++)
                            {
                                if (e.UnitTC(idx_0).Is(unitT) && e.UnitLevelTC(idx_0).Is(levUnit) && e.UnitPlayerTC(idx_0).Is(player))
                                {
                                    sMM.AttackUnitS.AttackUnit(1, e.NextPlayer(e.UnitPlayerTC(idx_0).Player).Player, idx_0, sMM, e);
                                    e.UnitTC(idx_0).Unit = UnitTypes.None;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}