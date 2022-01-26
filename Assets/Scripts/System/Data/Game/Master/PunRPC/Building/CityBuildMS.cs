using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireE;

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
                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;


                var whoseMove = Entities.WhoseMoveE.WhoseMove.Player;


                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(BuildingTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                    {
                        if (!Parent(idx_1).IsActiveSelf.IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        CellBuildEs.SetNew(forBuildType, whoseMove, idx_0);


                        CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(BuildingTypes.City));


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
