using Leopotam.Ecs;
using Game.Common;
using UnityEngine;

namespace Game.Game
{
    public sealed class ThirstyUpdMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, WaterUnitC> _statUnitF = default;
        private EcsFilter<UnitEffectsC> _effUnitF = default;

        private EcsFilter<RiverC> _cellRiverFilt = default;
        private EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;

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
                            water_0.SetMaxWater(UnitWaterUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit));
                        }
                        else
                        {
                            water_0.TakeWater();
                            if (!water_0.HaveWater)
                            {
                                hp_0.TakeHpThirsty(unit_0.Unit);

                                if (!hp_0.HaveHp)
                                {
                                    if (unit_0.Is(UnitTypes.King))
                                    {
                                        PlyerWinnerC.PlayerWinner = WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner);
                                    }
                                    else if (unit_0.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
                                    {
                                        ScoutHeroCooldownC.SetStandCooldown(ownUnit_0.Owner, unit_0.Unit);
                                        InvUnitsC.AddUnit(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level);
                                    }

                                    if (build_0.Is(BuildTypes.Camp))
                                    {
                                        WhereBuildsC.Remove(ownBuild_0.Owner, build_0.Build, idx_0);
                                        build_0.Remove();
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