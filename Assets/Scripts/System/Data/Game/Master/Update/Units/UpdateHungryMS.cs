namespace Game.Game
{
    sealed class UpdateHungryMS : SystemCellAbstract, IEcsRunSystem
    {
        public UpdateHungryMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = UnitEs;

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (Es.InventorResourcesEs.Resource(res, player).Resources.IsMinus)
                {
                    Es.InventorResourcesEs.Resource(res, player).Resources.Amount = 0;

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in CellEs.Idxs)
                            {
                                if (Es.WhereUnitsEs.WhereUnit(unit, levUnit, player, idx_0).HaveUnit.Have)
                                {
                                    var build_0 = BuildEs.BuildingE(idx_0).BuildTC;


                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        Es.WhereBuildingEs.HaveBuild(BuildEs.BuildingE(idx_0), idx_0).HaveBuilding.Have = false;
                                        BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);
                                    }

                                    UnitEs.Main(idx_0).Kill(Es);

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