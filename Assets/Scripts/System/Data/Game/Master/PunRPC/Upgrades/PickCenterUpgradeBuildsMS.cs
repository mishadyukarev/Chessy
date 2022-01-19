namespace Game.Game
{
    struct PickCenterUpgradeBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var build = EntityMPool.Build<BuildingTC>().Build;


            var whoseMove = WhoseMoveE.CurPlayerI;


            AvailableCenterUpgradeEs.HaveUpgrade<HaveUpgradeC>(whoseMove).Have = false;
            BuildingUpgradesEs.HaveUpgrade<HaveUpgradeC>(build, whoseMove, UpgradeTypes.PickCenter).Have = true;
            AvailableCenterUpgradeEs.HaveBuildUpgrade<HaveUpgradeC>(build, whoseMove).Have = false;

            EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}