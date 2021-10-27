using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class MeltOreMasterSystem : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            var playerSend = WhoseMoveC.WhoseMove;

            if (InventResourcesC.CanMeltOre(playerSend, out var needRes))
            {
                InventResourcesC.BuyMeltOre(playerSend);
                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Melting);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}