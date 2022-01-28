using System;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct SeedingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            var env = Entities.MasterEs.Seed<EnvironmetC>().Environment;
            var idx_0 = Entities.MasterEs.Seed<IdxC>().Idx;
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;

            ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;


            switch (env)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildingTypes.Camp))
                        {
                            Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have)
                            {
                                if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)

                                    if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Resources.Have)
                                    {
                                        Entities.Rpc.SoundToGeneral(sender, uniq_cur);

                                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).SetNew();

                                        Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                                    }
                                    else
                                    {
                                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                else
                                {
                                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }

                        }
                    }

                    else
                    {
                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
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
