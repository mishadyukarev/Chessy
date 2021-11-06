using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class ThirstyUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC> _cellUnitFilt = default;
        private EcsFilter<CellUnitDataCom, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;

        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _cellUnitOthFilt)
            {
                ref var hpUnitC_0 =ref _cellUnitFilt.Get2(idx_0);
                ref var unit_0 = ref _cellUnitOthFilt.Get1(idx_0);
                ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
                ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
                ref var effUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
                ref var thirUnitC_0 = ref _cellUnitOthFilt.Get3(idx_0);

                ref var riverC_0 = ref _cellRiverFilt.Get1(idx_0);

                ref var build_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuild_0 = ref _cellBuildFilt.Get2(idx_0);


                if (unit_0.HaveUnit)
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
                            thirUnitC_0.SetMaxWater(UnitPercUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Water));
                        }
                        else
                        {
                            thirUnitC_0.TakeWater();
                            if (!thirUnitC_0.HaveWater)
                            {
                                hpUnitC_0.TakeHpThirsty(unit_0.Unit);

                                if (!hpUnitC_0.HaveHp)
                                {
                                    if (unit_0.Is(UnitTypes.King))
                                    {
                                        EndGameDataUIC.PlayerWinner = WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner);
                                    }

                                    if (build_0.Is(BuildTypes.Camp))
                                    {
                                        WhereBuildsC.Remove(ownBuild_0.Owner, build_0.Build, idx_0);
                                        build_0.Reset();
                                    }

                                    WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);
                                    unit_0.NoneUnit();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}