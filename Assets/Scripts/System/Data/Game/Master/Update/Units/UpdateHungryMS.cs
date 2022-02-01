namespace Game.Game
{
    sealed class UpdateHungryMS : SystemCellAbstract, IEcsRunSystem
    {
        public UpdateHungryMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (Es.InventorResourcesEs.Resource(res, player).IsMinus)
                {
                    Es.InventorResourcesEs.Resource(res, player).Resources.Amount = 0;

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in CellEsWorker.Idxs)
                            {
                                if (Es.WhereUnitsEs.WhereUnit(unit, levUnit, player, idx_0).HaveUnit.Have)
                                {
                                    var build_0 = BuildEs(idx_0).BuildingE.BuildTC;


                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);
                                    }

                                    UnitEs(idx_0).MainE.Kill(Es);

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