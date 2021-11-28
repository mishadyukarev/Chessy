using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ExtractBuildUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (var build = BuildTypes.First; build < BuildTypes.End; build++)
                {
                    foreach (var idx_0 in WhereBuildsC.IdxBuilds(build, player))
                    {
                        ref var build_0 = ref EntityPool.Build<BuildC>(idx_0);
                        ref var ownBuild_0 = ref EntityPool.Build<OwnerC>(idx_0);

                        ref var env_0 = ref EntityPool.Environment<EnvC>(idx_0);
                        ref var envRes_0 = ref EntityPool.Environment<EnvResC>(idx_0);
                        ref var fire_0 = ref EntityPool.Fire<FireC>(idx_0);

                        var minus_0 = 0;


                        if (build_0.Is(BuildTypes.Farm))
                        {
                            minus_0 =  ExtractorC.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Farm, ownBuild_0.Owner));

                            if (minus_0 > envRes_0.Amount(EnvTypes.Fertilizer)) 
                                minus_0 = envRes_0.Amount(EnvTypes.Fertilizer);

                            envRes_0.Take(EnvTypes.Fertilizer, minus_0);
                            InvResC.Add(ResTypes.Food, ownBuild_0.Owner, minus_0);

                            if (!envRes_0.Have(EnvTypes.Fertilizer))
                            {
                                env_0.Remove(EnvTypes.Fertilizer);

                                build_0.Remove();
                            }
                        }

                        else if (build_0.Is(BuildTypes.Woodcutter))
                        {
                            minus_0 = ExtractorC.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Woodcutter, ownBuild_0.Owner));

                            if (minus_0 > envRes_0.Amount(EnvTypes.AdultForest))
                                minus_0 = envRes_0.Amount(EnvTypes.AdultForest);

                            envRes_0.Take(EnvTypes.AdultForest, minus_0);
                            InvResC.Add(ResTypes.Wood, ownBuild_0.Owner, minus_0);

                            if (!envRes_0.Have(EnvTypes.AdultForest))
                            {
                                env_0.Remove(EnvTypes.AdultForest);

                                build_0.Remove();

                                

                                if (fire_0.Have)
                                {
                                    fire_0.Disable();
                                }


                                if (UnityEngine.Random.Range(0, 100) < 50)
                                {
                                    EntityPool.Environment<EnvC>(idx_0).SetNew(EnvTypes.YoungForest);
                                }
                            }
                        }

                        else if (build_0.Is(BuildTypes.Mine))
                        {
                            minus_0 = ExtractorC.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Mine, ownBuild_0.Owner));

                            if (minus_0 > envRes_0.Amount(EnvTypes.Hill))
                                minus_0 = envRes_0.Amount(EnvTypes.Hill);

                            envRes_0.Take(EnvTypes.Hill, minus_0);
                            InvResC.Add(ResTypes.Ore, ownBuild_0.Owner, minus_0);

                            if (!envRes_0.Have(EnvTypes.Hill))
                            {
                                build_0.Remove();
                            }
                        }
                    }
                }
            }


        }
    }
}