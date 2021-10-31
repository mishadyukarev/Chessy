using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    public sealed class MeltOreMasterSystem : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MasGenOthTypes.Master);

            var playerSend = WhoseMoveC.WhoseMove;

            if (InventResC.CanMeltOre(playerSend, out var needRes))
            {
                InventResC.BuyMeltOre(playerSend);
                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Melting);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}