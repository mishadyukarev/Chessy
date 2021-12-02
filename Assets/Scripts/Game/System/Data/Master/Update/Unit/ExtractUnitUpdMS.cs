using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ExtractUnitUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionC>(idx_0);

                ref var env_0 = ref Environment<EnvC>(idx_0);
                ref var envRes_0 = ref Environment<EnvResC>(idx_0);

                ref var buil_0 = ref Build<BuildC>(idx_0);


                if (unit_0.CanExtract(out var extract, out var env, out var res))
                {
                    InvResC.Add(res, ownUnit_0.Owner, extract);
                    envRes_0.Take(env, extract);

                    if (env == EnvTypes.AdultForest)
                    {
                        if (envRes_0.Have(env))
                        {
                            if (buil_0.Is(BuildTypes.Camp) || !buil_0.Have)
                            {
                                buil_0.SetNew(BuildTypes.Woodcutter, ownUnit_0.Owner);
                            }

                            else if (!buil_0.Is(BuildTypes.Woodcutter))
                            {
                                condUnit_0.Set(CondUnitTypes.Protected);
                            }
                        }

                        else
                        {
                            buil_0.Remove();
                            env_0.Remove(env);

                            if (UnityEngine.Random.Range(0, 100) < 50)
                            {
                                env_0.SetNew(EnvTypes.YoungForest);
                            }
                        }
                    }
                }
                else if(!unit_0.CanResume(out extract, out env))
                {
                    if (Unit<HpC>(idx_0).HaveMax)
                    {
                        if (unit_0.Have && Unit<StepC>(idx_0).HaveMin)
                        {
                            condUnit_0.Set(CondUnitTypes.Protected);
                        }
                    }
                }
            }
        }
    }
}