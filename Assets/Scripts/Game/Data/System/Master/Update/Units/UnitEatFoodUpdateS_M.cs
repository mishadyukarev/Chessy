namespace Chessy.Game.System.Model
{
    sealed class UnitEatFoodUpdateS_M : SystemAbstract, IEcsRunSystem
    {
        internal UnitEatFoodUpdateS_M(in EntitiesModel ents) : base(ents) { }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (E.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                {
                    E.PlayerInfoE(player).ResourcesC(res).Resources = 0;

                    for (var unitT = UnitTypes.Elfemale; unitT >= UnitTypes.Pawn; unitT--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
                            {
                                if (E.UnitTC(idx_0).Is(unitT) && E.UnitLevelTC(idx_0).Is(levUnit) && E.UnitPlayerTC(idx_0).Is(player))
                                {
                                    AttackUnitS.AttackUnit(1, E.NextPlayer(E.UnitPlayerTC(idx_0).Player).Player, idx_0, E);
                                    E.UnitTC(idx_0).Unit = UnitTypes.None;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}