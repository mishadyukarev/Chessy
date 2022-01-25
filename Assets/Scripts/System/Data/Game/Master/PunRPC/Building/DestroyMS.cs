using Photon.Pun;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct DestroyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntityMPool.DestroyIdxC.Idx;

            ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;

            ref var buildC_0 = ref Build<BuildingTC>(idx_0);


            if (CellUnitEntities.Step(idx_0).AmountC.Have)
            {
                EntityPool.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    EntityPool.Winner.Player = ownUnit_0.Player;
                }
                CellUnitEntities.Step(idx_0).AmountC.Take();

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    Remove(EnvironmentTypes.Fertilizer, idx_0);
                }

                CellBuildE.Remove(idx_0);
            }
            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}