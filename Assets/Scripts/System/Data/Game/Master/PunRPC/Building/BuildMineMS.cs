using static Game.Game.CellEs;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using Game.Common;

namespace Game.Game
{
    struct BuildMineMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var build = EntityMPool.Build<BuildingTC>().Build;
            var idx_0 = EntityMPool.Build<IdxC>().Idx;


            ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;
            ref var ownBuild_0 = ref CellBuildEs.Build(idx_0).PlayerTC;


            var whoseMove = Entities.WhoseMoveE.WhoseMove.Player;

            if (build == BuildingTypes.Mine)
            {
                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(build))
                {
                    if (!build_0.Have || build_0.Is(BuildingTypes.Camp))
                    {
                        if (Environment(EnvironmentTypes.Hill, idx_0).Resources.Have
                            && Environment(EnvironmentTypes.Hill, idx_0).Resources.Have)
                        {
                            if (InventorResourcesE.CanCreateBuild(build, whoseMove, out var needRes))
                            {
                                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Building);

                                InventorResourcesE.BuyBuild(whoseMove, build);


                                CellBuildEs.SetNew(build, whoseMove, idx_0);

                                CellUnitEs.Step(idx_0).AmountC.Take();
                            }

                            else EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                        }

                        else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                    }
                    else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}