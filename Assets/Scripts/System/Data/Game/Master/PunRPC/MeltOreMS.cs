namespace Game.Game
{
    sealed class MeltOreMS : SystemAbstract, IEcsRunSystem
    {
        public MeltOreMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (Es.InventorResourcesEs.CanMeltOre(whoseMove, out var needRes))
            {
                Es.InventorResourcesEs.BuyMeltOre(whoseMove);
                Es.Rpc.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}