﻿using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class MeltOreMasterSystem : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (InvResC.CanMeltOre(whoseMove, out var needRes))
            {
                InvResC.BuyMeltOre(whoseMove);
                RpcSys.SoundToGeneral(sender, ClipGameTypes.Melting);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}