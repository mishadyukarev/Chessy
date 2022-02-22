namespace Game.Game
{
    sealed class UpdateHungryMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateHungryMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (E.PlayerE(player).ResourcesC(res).Resources < 0)
                {
                    E.PlayerE(player).ResourcesC(res).Resources = 0;

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
                            {
                                if(E.UnitTC(idx_0).HaveUnit && E.UnitLevelTC(idx_0).Is(levUnit) && E.UnitPlayerTC(idx_0).Is(player))
                                {
                                    if (E.BuildTC(idx_0).Is(BuildingTypes.Camp))
                                    {
                                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                                        //Es.BuildTC(idx_0).Destroy(Es);
                                    }

                                    if (E.UnitTC(idx_0).Is(UnitTypes.King))
                                    {
                                        E.WinnerC.Player = E.UnitPlayerTC(idx_0).Player;
                                    }
                                    else if (E.UnitTC(idx_0).Is(UnitTypes.Scout) || E.UnitMainE(idx_0).IsHero)
                                    {
                                        E.UnitInfo(E.UnitPlayerTC(idx_0).Player, LevelTypes.First, E.UnitTC(idx_0).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(E.UnitTC(idx_0).Unit);
                                        E.UnitInfo(E.UnitPlayerTC(idx_0).Player, LevelTypes.First, E.UnitTC(idx_0).Unit).HaveInInventor = true;
                                    }

                                    //Es.LastDiedUnitTC(idx_0).SetLastDied((Es.UnitTC(idx_0), Es.UnitLevelTC(idx_0), Es.UnitPlayerTC(idx_0)), Es.LastDiedLevelTC(idx_0), Es.LastDiedPlayerTC(idx_0));
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