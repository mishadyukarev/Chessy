namespace Game.Game
{
    struct MeltOreMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            if (InventorResourcesE.CanMeltOre(whoseMove, out var needRes))
            {
                InventorResourcesE.BuyMeltOre(whoseMove);
                EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}