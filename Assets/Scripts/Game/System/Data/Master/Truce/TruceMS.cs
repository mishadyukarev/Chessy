using Leopotam.Ecs;
using Photon.Pun;
using Game.Common;
using UnityEngine;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class TruceMS : IEcsRunSystem
    {
        private EcsFilter<EnvC, EnvResC> _cellEnvFilter = default;
        private EcsFilter<FireC> _cellFireFilter = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;

        public void Run()
        {
            int random;

            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);
                ref var tw_0 = ref EntityPool.UnitToolWeapon<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref EntityPool.UnitToolWeapon<LevelC>(idx_0);


                ref var build_0 = ref EntityPool.Build<BuildC>(idx_0);
                ref var env_0 = ref _cellEnvFilter.Get1(idx_0);
                ref var envRes_0 = ref _cellEnvFilter.Get2(idx_0);
                ref var curFireCom = ref _cellFireFilter.Get1(idx_0);
                ref var trail_0 = ref EntityPool.Trail<TrailC>(idx_0);


                curFireCom.Disable();


                trail_0.ResetAll();

                if (unit_0.Have)
                {
                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Owner);
                                tw_0.Reset();
                            }

                            unit_0.AddToInventor();
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Owner);
                            tw_0.Reset();
                        }

                        unit_0.AddToInventor();
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildTypes.Camp))
                    {
                        build_0.Remove();
                    }
                }

                else
                {
                    if (env_0.Have(EnvTypes.YoungForest))
                    {
                        env_0.Remove(EnvTypes.YoungForest);

                        env_0.SetNew(EnvTypes.AdultForest);
                    }

                    if (!env_0.Have(EnvTypes.Fertilizer)
                        && !env_0.Have(EnvTypes.Mountain)
                        && !env_0.Have(EnvTypes.AdultForest))
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            env_0.SetNew(EnvTypes.Fertilizer);
                        }
                    }
                }
            }
        }
    }
}