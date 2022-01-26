namespace Game.Game
{
    struct ReadyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var playerSend = sender.GetPlayer();

            Entities.Ready(playerSend).IsReadyC.IsReady = !Entities.Ready(playerSend).IsReadyC.IsReady;

            if (Entities.Ready(PlayerTypes.First).IsReadyC.IsReady
                && Entities.Ready(PlayerTypes.Second).IsReadyC.IsReady)
            {
                Entities.GameInfo.IsStartedGameC.IsStartedGame = true;
            }

            else
            {
                Entities.GameInfo.IsStartedGameC.IsStartedGame = false;
            }
        }
    }
}