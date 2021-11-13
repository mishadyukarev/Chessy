using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class TruceMasterSystem : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<BuildC> _cellBuildFilter = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvFilter = default;
        private EcsFilter<FireC> _cellFireFilter = default;
        private EcsFilter<CellC> _cellDataFilt = default;
        private EcsFilter<TrailC> _trailFilt = default;

        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitFilter = default;
        private EcsFilter<UnitC, ToolWeaponC> _cellUnitTwFilt = default;

        public void Run()
        {
            int random;

            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levUnit_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var ownUnit_0 = ref _cellUnitFilter.Get3(idx_0);
                ref var tw_0 = ref _cellUnitTwFilt.Get2(idx_0);

                ref var build_0 = ref _cellBuildFilter.Get1(idx_0);
                ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
                ref var envRes_0 = ref _cellEnvFilter.Get2(idx_0);
                ref var curFireCom = ref _cellFireFilter.Get1(idx_0);
                ref var trail_0 = ref _trailFilt.Get1(idx_0);


                curFireCom.DisableFire();


                trail_0.ResetAll();

                if (unit_0.HaveUnit)
                {
                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveToolWeap)
                            {
                                InvToolWeapC.AddAmountTools(ownUnit_0.Owner, tw_0.ToolWeapType, tw_0.LevelTWType);
                                tw_0.ToolWeapType = default;
                            }

                            InvUnitsC.AddUnit(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level);
                            WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                            unit_0.Reset();
                        }
                    }
                    else
                    {

                        if (tw_0.HaveToolWeap)
                        {
                            InvToolWeapC.AddAmountTools(ownUnit_0.Owner, tw_0.ToolWeapType, tw_0.LevelTWType);
                            tw_0.ToolWeapType = default;
                        }

                        InvUnitsC.AddUnit(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level);
                        WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                        unit_0.Reset();
                    }
                }


                if (build_0.HaveBuild)
                {
                    if (build_0.Is(BuildTypes.Camp))
                    {
                        WhereBuildsC.Remove(ownUnit_0.Owner, build_0.Build, idx_0);
                        build_0.Reset();
                    }
                }

                else
                {
                    if (env_0.Have(EnvTypes.YoungForest))
                    {
                        env_0.Reset(EnvTypes.YoungForest);
                        WhereEnvC.Remove(EnvTypes.YoungForest, idx_0);

                        env_0.Set(EnvTypes.AdultForest);
                        envRes_0.SetNew(EnvTypes.AdultForest);
                        WhereEnvC.Add(EnvTypes.AdultForest, idx_0);
                    }

                    if (!env_0.Have(EnvTypes.Fertilizer)
                        && !env_0.Have(EnvTypes.Mountain)
                        && !env_0.Have(EnvTypes.AdultForest))
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            env_0.Set(EnvTypes.Fertilizer);
                            envRes_0.SetNew(EnvTypes.Fertilizer);
                            WhereEnvC.Add(EnvTypes.Fertilizer, idx_0);
                        }
                    }
                }
            }
        }
    }
}