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

            ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;

            ref var buildC_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;


            if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
            {
                Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    Es.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    Es.CellEs.EnvironmentEs.Fertilizer( idx_0).Destroy(Es.WhereEnviromentEs);
                }

                Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                Es.CellEs.BuildEs.Build(idx_0).Remove();
            }
            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}