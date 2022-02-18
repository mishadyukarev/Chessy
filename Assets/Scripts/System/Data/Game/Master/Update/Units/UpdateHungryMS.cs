namespace Game.Game
{
    sealed class UpdateHungryMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateHungryMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (Es.PlayerE(player).ResourcesC(res).Resources < 0)
                {
                    Es.PlayerE(player).ResourcesC(res).Resources = 0;

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
                            {
                                if(Es.UnitTC(idx_0).HaveUnit && Es.UnitLevelTC(idx_0).Is(levUnit) && Es.UnitPlayerTC(idx_0).Is(player))
                                {
                                    if (Es.BuildTC(idx_0).Is(BuildingTypes.Camp))
                                    {
                                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                                        //Es.BuildTC(idx_0).Destroy(Es);
                                    }

                                    if (Es.UnitTC(idx_0).Is(UnitTypes.King))
                                    {
                                        Es.WinnerC.Player = Es.UnitPlayerTC(idx_0).Player;
                                    }
                                    else if (Es.UnitTC(idx_0).Is(UnitTypes.Scout) || Es.UnitEs(idx_0).IsHero)
                                    {
                                        Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).UnitsInfoE(Es.UnitTC(idx_0).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(Es.UnitTC(idx_0).Unit);
                                        Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).UnitsInfoE(Es.UnitTC(idx_0).Unit).HaveInInventor = true;
                                    }

                                    //Es.LastDiedUnitTC(idx_0).SetLastDied((Es.UnitTC(idx_0), Es.UnitLevelTC(idx_0), Es.UnitPlayerTC(idx_0)), Es.LastDiedLevelTC(idx_0), Es.LastDiedPlayerTC(idx_0));
                                    Es.UnitTC(idx_0).Unit = UnitTypes.None;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}