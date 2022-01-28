namespace Game.Game
{
    struct MeltOreMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (InventorResourcesE.CanMeltOre(whoseMove, out var needRes))
            {
                InventorResourcesE.BuyMeltOre(whoseMove);
                Entities.Rpc.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}