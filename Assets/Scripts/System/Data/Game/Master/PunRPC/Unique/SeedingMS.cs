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
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            var build_0 = BuildEs.BuildingE(idx_0).BuildTC;


            switch (env)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildingTypes.Camp))
                        {
                            Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!CellEs.EnvironmentEs.Fertilizer( idx_0).HaveEnvironment)
                            {
                                if (!CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)

                                    if (!CellEs.EnvironmentEs.YoungForest( idx_0).HaveEnvironment)
                                    {
                                        Es.Rpc.SoundToGeneral(sender, uniq_cur);

                                        CellEs.EnvironmentEs.YoungForest( idx_0).SetNew(Es.WhereEnviromentEs);

                                        UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq_cur);
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
