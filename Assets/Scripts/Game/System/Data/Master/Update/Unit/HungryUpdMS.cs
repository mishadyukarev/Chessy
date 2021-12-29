using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class HungryUpdMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;

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
                                ref var unit_0 = ref _unitF.Get1(idx_0);
                                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                                ref var buildCell_0 = ref Build<BuildCellC>(idx_0);
                                ref var build_0 = ref EntityCellPool.Build<BuildC>(idx_0);
                                ref var ownBuild_0 = ref EntityCellPool.Build<OwnerC>(idx_0);




                                if (build_0.Is(BuildTypes.Camp))
                                {
                                    buildCell_0.Remove();
                                }

                                EntityCellPool.Unit<UnitCellWC>(idx_0).Kill();

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}