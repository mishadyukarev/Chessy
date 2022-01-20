namespace Game.Game
{
    struct BuyResourcesMS : IEcsRunSystem
    {
        public void Run()
        {
            var res = EntityMPool.BuyResources<ResourceTypeC>().Resource;

            var sender = InfoC.Sender(MGOTypes.Master);


            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            if (InventorResourcesE.CanBuy(whoseMove, res, out var needRes))
            {
                InventorResourcesE.BuyRes(whoseMove, res);

                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}
