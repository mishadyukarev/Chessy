﻿using Leopotam.Ecs;

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

                                ref var build_0 = ref EntityPool.Build<BuildC>(idx_0);
                                ref var ownBuild_0 = ref EntityPool.Build<OwnerC>(idx_0);




                                if (build_0.Is(BuildTypes.Camp))
                                {
                                    build_0.Remove();
                                }

                                EntityPool.Unit<UnitCellC>(idx_0).Kill(levUnit_0.Level, ownUnit_0.Owner);

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}