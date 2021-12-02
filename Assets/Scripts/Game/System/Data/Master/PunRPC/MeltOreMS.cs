using Leopotam.Ecs;
using Photon.Pun;
using Game.Common;
using System.Collections.Generic;

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
                RpcSys.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}