using Leopotam.Ecs;
using Game.Common;
using UnityEngine;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class ThirstyUpdMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, WaterC> _statUnitF = default;
        private EcsFilter<EffectsC> _effUnitF = default;

        private EcsFilter<RiverC> _cellRiverFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _effUnitF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var hp_0 = ref _statUnitF.Get1(idx_0);
                ref var water_0 = ref _statUnitF.Get2(idx_0);

                ref var eff_0 = ref _effUnitF.Get1(idx_0);


                ref var riverC_0 = ref _cellRiverFilt.Get1(idx_0);

                ref var buildCell_0 = ref Build<BuildCellC>(idx_0);
                ref var build_0 = ref EntityCellPool.Build<BuildC>(idx_0);
                ref var ownBuild_0 = ref EntityCellPool.Build<OwnerC>(idx_0);


                if (unit_0.Have)
                {
                    var canExecute = false;
                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (riverC_0.HaveNearRiver)
                        {
                            Unit<WaterUnitC>(idx_0).SetMaxWater();
                        }
                        else
                        {
                            Unit<WaterUnitC>(idx_0).TakeWater();
                            if (!water_0.Have)
                            {
                                Unit<WaterUnitC>(idx_0).ExecuteThirsty();

                                if (!hp_0.Have)
                                {
                                    if (build_0.Is(BuildTypes.Camp))
                                    {
                                        buildCell_0.Remove();
                                    }

                                    Unit<UnitCellWC>(idx_0).Kill();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}