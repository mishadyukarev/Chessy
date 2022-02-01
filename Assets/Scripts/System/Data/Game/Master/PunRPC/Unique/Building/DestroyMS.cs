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

            var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

            var buildC_0 = BuildEs(idx_0).BuildingE.BuildTC;


            if (UnitStatEs(idx_0).StepE.HaveSteps)
            {
                Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildingTypes.City))
                {
                    Es.WinnerE.Winner.Player = ownUnit_0.Player;
                }
                UnitStatEs(idx_0).StepE.Take(Es.MasterEs.AbilityC.Ability);

                if (buildC_0.Is(BuildingTypes.Farm))
                {
                    EnvironmentEs(idx_0).Fertilizer.Destroy(Es.WhereEnviromentEs);
                }

                Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);
            }
            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}