using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct BuildCityMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var forBuildType = EntityMPool.Build<BuildingC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;



            if (forBuildType == BuildTypes.City)
            {
                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var build_0 = ref Build<BuildingC>(idx_0);
                ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                //var whoseMove = WhoseMoveC.WhoseMove;


                if (stepUnit_0.Have(BuildTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var xy in CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy))
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


                        //buildCell_0.SetNew(forBuildType, whoseMove);


                        stepUnit_0.Take(BuildTypes.City);


                        fire_0.Disable();


                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Remove();
                        Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).Remove();
                        Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).Remove();
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
