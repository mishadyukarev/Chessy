using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ReadyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var playerSend = sender.GetPlayer();

            Ready<IsReadyC>(playerSend).IsReady = !Ready<IsReadyC>(playerSend).IsReady;

            if (Ready<IsReadyC>(PlayerTypes.First).IsReady 
                && Ready<IsReadyC>(PlayerTypes.Second).IsReady)
            {
                GameInfo<IsStartedGameC>().IsStartedGame = true;
            }

            else
            {
                GameInfo<IsStartedGameC>().IsStartedGame = false;
            }
        }
    }
}