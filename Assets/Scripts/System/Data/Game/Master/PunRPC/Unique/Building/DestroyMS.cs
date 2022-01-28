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
            var idx_0 = Entities.MasterEs.DestroyIdxC.Idx;

            ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

            ref var buildC_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;


            if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
            {
                Entities.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    Entities.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).Remove();
                }

                Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                Entities.CellEs.BuildEs.Build(idx_0).Remove();
            }
            else
            {
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}