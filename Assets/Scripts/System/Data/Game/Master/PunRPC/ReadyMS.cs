namespace Game.Game
{
    sealed class ReadyMS : SystemAbstract, IEcsRunSystem
    {
        public ReadyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var playerSend = sender.GetPlayer();

            Es.Ready(playerSend).IsReadyC.IsReady = !Es.Ready(playerSend).IsReadyC.IsReady;

            if (Es.Ready(PlayerTypes.First).IsReadyC.IsReady
                && Es.Ready(PlayerTypes.Second).IsReadyC.IsReady)
            {
                Es.GameInfo.IsStartedGameC.IsStartedGame = true;
            }

            else
            {
                Es.GameInfo.IsStartedGameC.IsStartedGame = false;
            }
        }
    }
}