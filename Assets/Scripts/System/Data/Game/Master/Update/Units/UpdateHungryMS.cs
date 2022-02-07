namespace Game.Game
{
    sealed class UpdateHungryMS : SystemCellAbstract, IEcsRunSystem
    {
        internal UpdateHungryMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (Es.InventorResourcesEs.Resource(res, player).IsMinus)
                {
                    Es.InventorResourcesEs.Resource(res, player).Reset();

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
                            {
                                if(Es.UnitTypeE(idx_0).HaveUnit && Es.UnitLevelE(idx_0).Is(levUnit) && Es.UnitOwnerE(idx_0).OwnerC.Is(player))
                                {
                                    if (Es.BuildE(idx_0).Is(BuildingTypes.Camp))
                                    {
                                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                                        Es.BuildE(idx_0).Destroy(Es);
                                    }

                                    Es.UnitTypeE(idx_0).Kill(Es);

                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}