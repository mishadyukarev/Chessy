using static Game.Game.CellBuildE;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct UpdateHungryMS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResTypes.Food;



                if (InventorResourcesE.Resource<AmountC>(res, player).IsMinus)
                {
                    InventorResourcesE.Resource<AmountC>(res, player).Reset();

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in Idxs)
                            {
                                if (EntWhereUnits.HaveUnit<HaveUnitC>(unit, levUnit, player, idx_0).Have)
                                {
                                    ref var build_0 = ref Build<BuildingTC>(idx_0);


                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        CellBuildE.Remove(idx_0);
                                    }

                                    CellUnitEs.Kill(idx_0);

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