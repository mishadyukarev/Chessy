using Photon.Pun;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct DestroyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntitiesMaster.DestroyIdxC.Idx;

            ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

            ref var buildC_0 = ref CellBuildEs.Build(idx_0).BuildTC;


            if (CellUnitEs.Step(idx_0).AmountC.Have)
            {
                Entities.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

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
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}