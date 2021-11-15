﻿using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ReadyMasterSystem : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var playerSend = sender.GetPlayerType();


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