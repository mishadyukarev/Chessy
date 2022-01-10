﻿namespace Game.Game
{
    sealed class ReadyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var playerSend = sender.GetPlayer();


            ReadyC.SetIsReady(playerSend, !ReadyC.IsReady(playerSend));

            if (ReadyC.IsReady(PlayerTypes.First) && ReadyC.IsReady(PlayerTypes.Second))
            {
                ReadyC.IsStartedGame = true;
            }

            else
            {
                ReadyC.IsStartedGame = false;
            }
        }
    }
}