using System;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct SeedingMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            var env = EntityMPool.Seed<EnvironmetC>().Environment;
            var idx_0 = EntityMPool.Seed<IdxC>().Idx;
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

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
                            EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have)
                            {
                                if (!Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)

                                    if (!Environment(EnvironmentTypes.YoungForest, idx_0).Resources.Have)
                                    {
                                        EntityPool.Rpc.SoundToGeneral(sender, uniq_cur);

                                        SetNew(EnvironmentTypes.YoungForest, idx_0);

                                        CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                                    }
                                    else
                                    {
                                        EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                    }
                                else
                                {
                                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                                }
                            }
                            else
                            {
                                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                            }

                        }
                    }

                    else
                    {
                        EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
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
