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

                if (Es.InventorResourcesEs.Resource(res, player).ResourceC.IsMinus)
                {
                    Es.InventorResourcesEs.Resource(res, player).ResourceC.Reset();

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
                            {
                                if(Es.UnitTC(idx_0).HaveUnit && Es.UnitLevelTC(idx_0).Is(levUnit) && Es.UnitPlayerTC(idx_0).Is(player))
                                {
                                    if (Es.BuildingE(idx_0).Is(BuildingTypes.Camp))
                                    {
                                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                                        Es.BuildingE(idx_0).Destroy(Es);
                                    }

                                    if (Es.UnitTC(idx_0).Is(UnitTypes.King))
                                    {
                                        Es.WinnerC.Player = Es.UnitPlayerTC(idx_0).Player;
                                    }
                                    else if (Es.UnitTC(idx_0).Is(UnitTypes.Scout) || Es.UnitTC(idx_0).IsHero)
                                    {
                                        Es.ScoutHeroCooldownE(Es.UnitTC(idx_0).Unit, Es.UnitPlayerTC(idx_0).Player).CooldownC.Amount = ScoutHeroCooldownValues.AfterKill(Es.UnitTC(idx_0).Unit);
                                        Es.Units(Es.UnitTC(idx_0).Unit, Es.UnitLevelTC(idx_0).Level, Es.UnitPlayerTC(idx_0).Player).AmountC.Add(1);
                                    }

                                    Es.UnitEs(idx_0).WhoLastDiedHereE.SetLastDied(Es.UnitE(idx_0));
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