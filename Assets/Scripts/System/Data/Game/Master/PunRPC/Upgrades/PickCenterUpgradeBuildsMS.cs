namespace Game.Game
{
    struct PickCenterUpgradeBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var build = Entities.MasterEs.Build<BuildingTC>().Build;


            var whoseMove = Entities.WhoseMove.CurPlayerI;


            Entities.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            Entities.HaveUpgrade(build, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            Entities.AvailableCenterUpgradeEs.HaveBuildUpgrade(build, whoseMove).HaveUpgrade.Have = false;

            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}