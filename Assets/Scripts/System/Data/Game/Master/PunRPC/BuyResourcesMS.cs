namespace Game.Game
{
    struct BuyResourcesMS : IEcsRunSystem
    {
        public void Run()
        {
            var res = EntitiesMaster.BuyResources<ResourceTypeC>().Resource;

            var sender = InfoC.Sender(MGOTypes.Master);


            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (InventorResourcesE.CanBuy(whoseMove, res, out var needRes))
            {
                InventorResourcesE.BuyRes(whoseMove, res);

                Entities.Rpc.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}
