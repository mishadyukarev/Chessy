using System;

namespace Game.Game
{
    sealed class SeedingMS : SystemAbstract, IEcsRunSystem
    {
        public SeedingMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            var env = Es.MasterEs.Seed<EnvironmetTC>().Environment;
            var idx_0 = Es.MasterEs.Seed<IdxC>().Idx;
            var uniq_cur = Es.MasterEs.AbilityC.Ability;

            var build_0 = BuildEs(idx_0).BuildingE.BuildTC;


            switch (env)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (UnitStatEs(idx_0).StepE.Have(uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildingTypes.Camp))
                        {
                            Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!EnvironmentEs(idx_0).Fertilizer.HaveEnvironment)
                            {
                                if (!EnvironmentEs(idx_0).AdultForest.HaveEnvironment)

                                    if (!EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                                    {
                                        Es.Rpc.SoundToGeneral(sender, uniq_cur);

                                        EnvironmentEs(idx_0).YoungForest.SetNewRandom(Es.WhereEnviromentEs);

                                        UnitStatEs(idx_0).StepE.Take(uniq_cur);
                                    }
                                    else
                                    {
                                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                else
                                {
                                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }

                        }
                    }

                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                    break;

                case EnvironmentTypes.AdultForest:
                    throw new Exception();

                case EnvironmentTypes.Hill:
                    throw new Exception();

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
    }
}
