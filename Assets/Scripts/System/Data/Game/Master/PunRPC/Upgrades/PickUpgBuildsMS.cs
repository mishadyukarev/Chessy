namespace Game.Game
{
    struct PickUpgBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            BuildDoingMC.Get(out var build);

            var whoseMove = WhoseMoveC.WhoseMove;

            BuildsUpgC.AddUpgrade(build, whoseMove, UpgTypes.PickCenter);

            BuildAvailPickUpgC.Set(build, whoseMove, false);
            PickUpgC.SetHaveUpgrade(whoseMove, false);
            EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}