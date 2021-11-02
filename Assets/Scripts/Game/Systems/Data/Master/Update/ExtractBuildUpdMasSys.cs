using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class ExtractBuildUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellbuildFilt = default;
        private EcsFilter<CellFireDataC> _cellFireFilt = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;

        public void Run()
        {
            for (var player = Support.MinPlayerType; player < Support.MaxPlayerType; player++)
            {
                for (var build = Support.MinBuildType; build < Support.MaxBuildType; build++)
                {
                    foreach (var idx_0 in WhereBuildsC.IdxBuilds(player, build))
                    {
                        ref var build_0 = ref _cellbuildFilt.Get1(idx_0);
                        ref var ownBuild_0 = ref _cellbuildFilt.Get2(idx_0);

                        ref var env_0 = ref _cellEnvFilt.Get1(idx_0);
                        ref var envRes_0 = ref _cellEnvFilt.Get2(idx_0);
                        ref var fire_0 = ref _cellFireFilt.Get1(idx_0);

                        var minus_0 = 0;


                        if (build_0.Is(BuildTypes.Farm))
                        {
                            minus_0 =  ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(ownBuild_0.Owner, BuildTypes.Farm));

                            if (minus_0 > envRes_0.AmountRes(EnvTypes.Fertilizer)) 
                                minus_0 = envRes_0.AmountRes(EnvTypes.Fertilizer);

                            envRes_0.TakeAmountRes(EnvTypes.Fertilizer, minus_0);
                            InventResC.AddAmountRes(ownBuild_0.Owner, ResTypes.Food, minus_0);

                            if (!envRes_0.HaveRes(EnvTypes.Fertilizer))
                            {
                                env_0.Reset(EnvTypes.Fertilizer);
                                WhereEnvC.Remove(EnvTypes.Fertilizer, idx_0);

                                WhereBuildsC.Remove(ownBuild_0.Owner, build_0.BuildType, idx_0);
                                build_0.Reset();
                            }
                        }

                        else if (build_0.Is(BuildTypes.Woodcutter))
                        {
                            minus_0 = ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(ownBuild_0.Owner, BuildTypes.Woodcutter));

                            if (minus_0 > envRes_0.AmountRes(EnvTypes.AdultForest))
                                minus_0 = envRes_0.AmountRes(EnvTypes.AdultForest);

                            envRes_0.TakeAmountRes(EnvTypes.AdultForest, minus_0);
                            InventResC.AddAmountRes(ownBuild_0.Owner, ResTypes.Wood, minus_0);

                            if (!envRes_0.HaveRes(EnvTypes.AdultForest))
                            {
                                env_0.Reset(EnvTypes.AdultForest);
                                WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);

                                WhereBuildsC.Remove(ownBuild_0.Owner, build_0.BuildType, idx_0);
                                build_0.Reset();

                                _cellTrailFilt.Get1(idx_0).ResetAll();

                                if (fire_0.HaveFire)
                                {
                                    fire_0.HaveFire = false;
                                }
                            }
                        }

                        else if (build_0.Is(BuildTypes.Mine))
                        {
                            minus_0 = ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(ownBuild_0.Owner, BuildTypes.Mine));

                            if (minus_0 > envRes_0.AmountRes(EnvTypes.Hill))
                                minus_0 = envRes_0.AmountRes(EnvTypes.Hill);

                            envRes_0.TakeAmountRes(EnvTypes.Hill, minus_0);
                            InventResC.AddAmountRes(ownBuild_0.Owner, ResTypes.Ore, minus_0);

                            if (!envRes_0.HaveRes(EnvTypes.Hill))
                            {
                                WhereBuildsC.Remove(ownBuild_0.Owner, build_0.BuildType, idx_0);
                                build_0.Reset();
                            }
                        }
                    }
                }
            }


        }
    }
}