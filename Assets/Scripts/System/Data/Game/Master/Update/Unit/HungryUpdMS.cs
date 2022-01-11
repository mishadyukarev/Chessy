using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityCellBuildPool;

namespace Game.Game
{
    struct HungryUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResTypes.Food;



                if (InvResC.IsMinusRes(res, player))
                {
                    InvResC.Reset(res, player);

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in WhereUnitsC.Idxs(unit, levUnit, player))
                            {
                                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                                ref var build_0 = ref Build<BuildC>(idx_0);



                                if (build_0.Is(BuildTypes.Camp))
                                {
                                    buildCell_0.Remove();
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