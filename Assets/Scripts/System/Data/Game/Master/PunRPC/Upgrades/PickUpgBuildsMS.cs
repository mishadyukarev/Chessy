namespace Game.Game
{
    struct PickUpgBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var build = EntityMPool.Build<BuildingTC>().Build;


            var whoseMove = WhoseMoveE.CurPlayerI;


            AvailableCenterUpgradeEs.HaveUpgrade<HaveUpgradeC>(whoseMove).Have = false;

            //EntBuildUpgrades.Upgrade<HaveUpgradeC>(build, whoseMove, UpgradeTypes.PickCenter).Have = true;

            //BuildAvailPickUpgC.Set(build, whoseMove, false);
            //PickUpgC.SetHaveUpgrade(whoseMove, false);
            EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}