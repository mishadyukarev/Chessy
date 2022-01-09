namespace Game.Game
{
    public sealed class MeltOreMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (InvResC.CanMeltOre(whoseMove, out var needRes))
            {
                InvResC.BuyMeltOre(whoseMove);
                RpcS.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                RpcS.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}