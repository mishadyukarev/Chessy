using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct CityBuildMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var forBuildType = EntitiesMaster.Build<BuildingTC>().Build;
            var idx_0 = EntitiesMaster.Build<IdxC>().Idx;



            if (forBuildType == BuildingTypes.City)
            {
                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;


                var whoseMove = Entities.WhoseMove.WhoseMove.Player;


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
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        CellBuildEs.SetNew(forBuildType, whoseMove, idx_0);


                        CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(BuildingTypes.City));


                        fire_0.Disable();


                        Remove(EnvironmentTypes.AdultForest, idx_0);
                        Remove(EnvironmentTypes.Fertilizer, idx_0);
                        Remove(EnvironmentTypes.YoungForest, idx_0);
                    }

                    else
                    {
                        Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                    }
                }
                else
                {
                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
