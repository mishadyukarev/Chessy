namespace Game.Game
{
    sealed class BuyResourcesMS : SystemAbstract, IEcsRunSystem
    {
        public BuyResourcesMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var res = Es.MasterEs.BuyResources<ResourceTC>().Resource;

            var sender = InfoC.Sender(MGOTypes.Master);


            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (Es.InventorResourcesEs.CanBuy(whoseMove, res, out var needRes))
            {
                Es.InventorResourcesEs.BuyRes(whoseMove, res);

                Es.Rpc.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}
