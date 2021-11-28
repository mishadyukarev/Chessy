using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ExtractBuildUpdMS : IEcsRunSystem
    {
        private EcsFilter<EnvC, EnvResC> _cellEnvFilt = default;
        private EcsFilter<FireC> _cellFireFilt = default;

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (var build = BuildTypes.First; build < BuildTypes.End; build++)
                {
                    foreach (var idx_0 in WhereBuildsC.IdxBuilds(build, player))
                    {
                        ref var build_0 = ref EntityPool.BuildCellC<BuildC>(idx_0);
                        ref var ownBuild_0 = ref EntityPool.BuildCellC<OwnerC>(idx_0);

                        ref var env_0 = ref _cellEnvFilt.Get1(idx_0);
                        ref var envRes_0 = ref _cellEnvFilt.Get2(idx_0);
                        ref var fire_0 = ref _cellFireFilt.Get1(idx_0);

                        var minus_0 = 0;


                        if (build_0.Is(BuildTypes.Farm))
                        {
                            minus_0 =  Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Farm, ownBuild_0.Owner));

                            if (minus_0 > envRes_0.AmountRes(EnvTypes.Fertilizer)) 
                                minus_0 = envRes_0.AmountRes(EnvTypes.Fertilizer);

                            envRes_0.TakeAmountRes(EnvTypes.Fertilizer, minus_0);
                            InvResC.Add(ResTypes.Food, ownBuild_0.Owner, minus_0);

                            if (!envRes_0.HaveRes(EnvTypes.Fertilizer))
                            {
                                env_0.Remove(EnvTypes.Fertilizer);

                                build_0.Remove(ownBuild_0.Owner);
                            }
                        }

                        else if (build_0.Is(BuildTypes.Woodcutter))
                        {
                            minus_0 = Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Woodcutter, ownBuild_0.Owner));

                            if (minus_0 > envRes_0.AmountRes(EnvTypes.AdultForest))
                                minus_0 = envRes_0.AmountRes(EnvTypes.AdultForest);

                            envRes_0.TakeAmountRes(EnvTypes.AdultForest, minus_0);
                            InvResC.Add(ResTypes.Wood, ownBuild_0.Owner, minus_0);

                            if (!envRes_0.HaveRes(EnvTypes.AdultForest))
                            {
                                env_0.Remove(EnvTypes.AdultForest);

                                build_0.Remove(ownBuild_0.Owner);

                                EntityPool.TrailCellC<TrailC>(idx_0).ResetAll();

                                if (fire_0.Have)
                                {
                                    fire_0.Disable();
                                }


                                if (UnityEngine.Random.Range(0, 100) < 50)
                                {
                                    ref var envDatCom = ref _cellEnvFilt.Get1(idx_0);

                                    envDatCom.SetNew(EnvTypes.YoungForest);
                                }
                            }
                        }

                        else if (build_0.Is(BuildTypes.Mine))
                        {
                            minus_0 = Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Mine, ownBuild_0.Owner));

                            if (minus_0 > envRes_0.AmountRes(EnvTypes.Hill))
                                minus_0 = envRes_0.AmountRes(EnvTypes.Hill);

                            envRes_0.TakeAmountRes(EnvTypes.Hill, minus_0);
                            InvResC.Add(ResTypes.Ore, ownBuild_0.Owner, minus_0);

                            if (!envRes_0.HaveRes(EnvTypes.Hill))
                            {
                                build_0.Remove(ownBuild_0.Owner);
                            }
                        }
                    }
                }
            }


        }
    }
}