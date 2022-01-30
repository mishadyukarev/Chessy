namespace Game.Game
{
    sealed class UpdateHungryMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateHungryMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = Es.CellEs.UnitEs;

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;

                if (Es.InventorResourcesEs.Resource(res, player).Resources.IsMinus)
                {
                    Es.InventorResourcesEs.Resource(res, player).Resources.Reset();

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in Es.CellEs.Idxs)
                            {
                                if (Es.WhereUnitsEs.WhereUnit(unit, levUnit, player, idx_0).HaveUnit.Have)
                                {
                                    ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;


                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                                        Es.CellEs.BuildEs.Build(idx_0).Remove();
                                    }

                                    Es.CellEs.UnitEs.Kill(idx_0, Es);

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