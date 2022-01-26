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


            var env = EntitiesMaster.Seed<EnvironmetC>().Environment;
            var idx_0 = EntitiesMaster.Seed<IdxC>().Idx;
            var uniq_cur = EntitiesMaster.UniqueAbilityC.Ability;

            ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;


            switch (env)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildingTypes.Camp))
                        {
                            Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have)
                            {
                                if (!Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)

                                    if (!Environment(EnvironmentTypes.YoungForest, idx_0).Resources.Have)
                                    {
                                        Entities.Rpc.SoundToGeneral(sender, uniq_cur);

                                        SetNew(EnvironmentTypes.YoungForest, idx_0);

                                        CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
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
