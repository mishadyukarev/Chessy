using System;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
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

            ref var build_0 = ref Build<BuildingTC>(idx_0);


            switch (env)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    throw new Exception();

                case EnvironmentTypes.YoungForest:
                    if (EntitiesPool.UnitStep.Have(idx_0, uniq_cur))
                    {
                        if (build_0.Have && !build_0.Is(BuildingTypes.Camp))
                        {
                            EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                        }
                        else
                        {
                            if (!Resources(EnvironmentTypes.Fertilizer, idx_0).Have)
                            {
                                if (!Resources(EnvironmentTypes.AdultForest, idx_0).Have)

                                    if (!Resources(EnvironmentTypes.YoungForest, idx_0).Have)
                                    {
                                        EntityPool.Rpc.SoundToGeneral(sender, uniq_cur);

                                        SetNew(EnvironmentTypes.YoungForest, idx_0);

                                        EntitiesPool.UnitStep.Take(idx_0, uniq_cur);
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
