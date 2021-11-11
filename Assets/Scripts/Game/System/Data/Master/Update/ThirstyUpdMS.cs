using Leopotam.Ecs;
using Chessy.Common;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class ThirstyUpdMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC> _cellUnitFilt = default;
        private EcsFilter<UnitC, UnitEffectsC, WaterUnitC> _cellUnitOthFilt = default;

        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;
        private EcsFilter<CellBuildDataC, OwnerC> _cellBuildFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _cellUnitOthFilt)
            {
                ref var hp_0 =ref _cellUnitFilt.Get2(idx_0);
                ref var unit_0 = ref _cellUnitOthFilt.Get1(idx_0);
                ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
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
                                hp_0.TakeHpThirsty(unit_0.Unit);

                                if (!hp_0.HaveHp)
                                {
                                    if (unit_0.Is(UnitTypes.King))
                                    {
                                        EndGameDataUIC.PlayerWinner = WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner);
                                    }
                                    else if (unit_0.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
                                    {
                                        ScoutHeroCooldownC.SetStandCooldown(ownUnit_0.Owner, unit_0.Unit);
                                        InvUnitsC.AddUnit(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level);
                                    }

                                    if (build_0.Is(BuildTypes.Camp))
                                    {
                                        WhereBuildsC.Remove(ownBuild_0.Owner, build_0.Build, idx_0);
                                        build_0.Reset();
                                    }

                                    WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                                    unit_0.Reset();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}