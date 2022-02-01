namespace Game.Game
{
    sealed class PickCenterUpgradeBuildsMS : SystemAbstract, IEcsRunSystem
    {
        public PickCenterUpgradeBuildsMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var build = Es.MasterEs.CenterUpgradeME.BuildingForUpgrade.Build;


            var whoseMove = Es.WhoseMove.CurPlayerI;


            Es.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            Es.BuildingUpgradeEs.HaveUpgrade(build, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            Es.AvailableCenterUpgradeEs.HaveBuildUpgrade(build, whoseMove).HaveUpgrade.Have = false;

            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}