using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
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

                ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;


                if (CellUnitStepEs.Have(idx_0, BuildingTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpaceC.GetXyAround(Cell<XyC>(idx_0).Xy))
                    {
                        var curIdx = IdxCell(xy);

                        if (!Cell<IsActiveC>(curIdx).IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Building);
                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        CellBuildE.SetNew(forBuildType, whoseMove, idx_0);


                        CellUnitStepEs.Take(idx_0, BuildingTypes.City);


                        fire_0.Disable();


                        Remove(EnvironmentTypes.AdultForest, idx_0);
                        Remove(EnvironmentTypes.Fertilizer, idx_0);
                        Remove(EnvironmentTypes.YoungForest, idx_0);
                    }

                    else
                    {
                        EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                    }
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}
