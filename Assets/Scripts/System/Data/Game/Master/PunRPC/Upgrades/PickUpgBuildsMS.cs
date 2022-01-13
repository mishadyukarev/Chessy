namespace Game.Game
{
    struct PickUpgBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            //BuildDoingMC.Get(out var build);

            //var whoseMove = WhoseMoveC.WhoseMove;

            //EntBuildUpgrades.Upgrade<HaveUpgradeC>(build, whoseMove, UpgradeTypes.PickCenter).Have = true;

            ////BuildAvailPickUpgC.Set(build, whoseMove, false);
            ////PickUpgC.SetHaveUpgrade(whoseMove, false);
            //EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}