using Photon.Pun;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct DestroyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntityMPool.DestroyIdxC.Idx;

            ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

            ref var buildC_0 = ref CellBuildEs.Build(idx_0).BuildTC;


            if (CellUnitEs.Step(idx_0).AmountC.Have)
            {
                EntityPool.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    Entities.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                CellUnitEs.Step(idx_0).AmountC.Take();

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    Remove(EnvironmentTypes.Fertilizer, idx_0);
                }

                CellBuildEs.Remove(idx_0);
            }
            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}