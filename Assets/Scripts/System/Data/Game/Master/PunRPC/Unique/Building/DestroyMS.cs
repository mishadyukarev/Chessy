using Photon.Pun;

namespace Game.Game
{
    sealed class DestroyMS : SystemCellAbstract, IEcsRunSystem
    {
        public DestroyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = Es.MasterEs.DestroyIdxC.Idx;

            var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;

            var buildC_0 = BuildEs.BuildingE(idx_0).BuildTC;


            if (UnitEs.StatEs.Step(idx_0).HaveSteps)
            {
                Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    Es.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                UnitEs.StatEs.Step(idx_0).Steps.Amount--;

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    CellEs.EnvironmentEs.Fertilizer( idx_0).Destroy(Es.WhereEnviromentEs);
                }

                Es.WhereBuildingEs.HaveBuild(BuildEs.BuildingE(idx_0), idx_0).HaveBuilding.Have = false;
                BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);
            }
            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}