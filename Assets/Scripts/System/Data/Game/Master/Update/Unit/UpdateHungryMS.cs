using static Game.Game.CellEs;

namespace Game.Game
{
    struct UpdateHungryMS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResourceTypes.Food;



                if (InventorResourcesE.Resource(res, player).IsMinus)
                {
                    InventorResourcesE.Resource(res, player).Reset();

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in Entities.CellEs.Idxs)
                            {
                                if (WhereUnitsE.HaveUnit(unit, levUnit, player, idx_0).Have)
                                {
                                    ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;


                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                                        Entities.CellEs.BuildEs.Build(idx_0).Remove();
                                    }

                                    Entities.CellEs.UnitEs.Kill(idx_0);

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