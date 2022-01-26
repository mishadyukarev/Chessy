namespace Game.Game
{
    struct PickCenterUpgradeBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var build = EntitiesMaster.Build<BuildingTC>().Build;


            var whoseMove = Entities.WhoseMoveE.CurPlayerI;


            AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            BuildingUpgradesEs.HaveUpgrade<HaveUpgradeC>(build, whoseMove, UpgradeTypes.PickCenter).Have = true;
            AvailableCenterUpgradeEs.HaveBuildUpgrade(build, whoseMove).HaveUpgrade.Have = false;

            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}