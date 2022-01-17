﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;

namespace Game.Game
{
    struct HungryUpdMS : IEcsRunSystem
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
                                if(EntWhereUnits.HaveUnit<HaveUnitC>(unit, levUnit, player, idx_0).Have)
                                {
                                    ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                                    ref var build_0 = ref Build<BuildingTC>(idx_0);


                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        CellBuildE.Remove(idx_0);
                                    }

                                    Unit<UnitCellEC>(idx_0).Kill();

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