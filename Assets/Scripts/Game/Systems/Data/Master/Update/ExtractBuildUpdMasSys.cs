using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class ExtractBuildUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<CellEnvDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellbuildFilt = default;
        private EcsFilter<CellFireDataC> _cellFireFilt = default;

        public void Run()
        {
            for (var player = Support.MinPlayerType; player < Support.MaxPlayerType; player++)
            {
                for (var build = Support.MinBuildType; build < Support.MaxBuildType; build++)
                {
                    foreach (var idx_0 in WhereBuildsC.IdxBuilds(player, build))
                    {
                        ref var env_0 = ref _cellEnvFilt.Get1(idx_0);

                        ref var buil_0 = ref _cellbuildFilt.Get1(idx_0);
                        ref var ownBuil_0 = ref _cellbuildFilt.Get2(idx_0);

                        ref var fire_0 = ref _cellFireFilt.Get1(idx_0);

                        var minus = 0;

                        if (buil_0.HaveBuild)
                        {
                            if (buil_0.Is(BuildTypes.Farm))
                            {
                                minus = UpgBuildsC.GetExtractOneBuild(ownBuil_0.Owner, BuildTypes.Farm);

                                env_0.TakeAmountRes(EnvTypes.Fertilizer, minus);
                                InventResC.AddAmountRes(ownBuil_0.Owner, ResTypes.Food, minus);

                                if (!env_0.HaveRes(EnvTypes.Fertilizer))
                                {
                                    env_0.Reset(EnvTypes.Fertilizer);
                                    WhereEnvC.Remove(EnvTypes.Fertilizer, idx_0);

                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                    buil_0.NoneBuild();
                                }
                            }

                            else if (buil_0.Is(BuildTypes.Woodcutter))
                            {
                                minus = UpgBuildsC.GetExtractOneBuild(ownBuil_0.Owner, BuildTypes.Woodcutter);

                                env_0.TakeAmountRes(EnvTypes.AdultForest, minus);
                                InventResC.AddAmountRes(ownBuil_0.Owner, ResTypes.Wood, minus);

                                if (!env_0.HaveRes(EnvTypes.AdultForest))
                                {
                                    env_0.Reset(EnvTypes.AdultForest);
                                    WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);

                                    //SpawnNewSeed(idx_0);

                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                    buil_0.NoneBuild();

                                    if (fire_0.HaveFire)
                                    {
                                        fire_0.HaveFire = false;
                                    }
                                }
                            }

                            else if (buil_0.Is(BuildTypes.Mine))
                            {
                                minus = UpgBuildsC.GetExtractOneBuild(ownBuil_0.Owner, BuildTypes.Mine);

                                env_0.TakeAmountRes(EnvTypes.Hill, minus);
                                InventResC.AddAmountRes(ownBuil_0.Owner, ResTypes.Ore, minus);

                                if (!env_0.HaveRes(EnvTypes.Hill))
                                {
                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                    buil_0.NoneBuild();
                                }
                            }
                        }

                    }
                }
            }


        }
    }
}