using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;

namespace Game.Game
{
    struct CityBuildMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var forBuildType = EntityMPool.Build<BuildingTC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;



            if (forBuildType == BuildingTypes.City)
            {
                ref var build_0 = ref Build<BuildingTC>(idx_0);
                ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                var whoseMove = WhoseMoveE.WhoseMove.Player;


                if (CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(BuildingTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                    {
                        if (!IsActiveC(idx_1).IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        CellBuildE.SetNew(forBuildType, whoseMove, idx_0);


                        CellUnitEntities.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(BuildingTypes.City));


                        fire_0.Disable();


                        Remove(EnvironmentTypes.AdultForest, idx_0);
                        Remove(EnvironmentTypes.Fertilizer, idx_0);
                        Remove(EnvironmentTypes.YoungForest, idx_0);
                    }

                    else
                    {
                        EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                    }
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
