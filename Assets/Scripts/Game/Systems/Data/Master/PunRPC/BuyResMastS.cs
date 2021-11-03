using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public class BuyResMastS : IEcsRunSystem
    {
        public void Run()
        {
            var res = ForBuyResMasC.Res;

            var sender = InfoC.Sender(MGOTypes.Master);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (InventResC.CanBuyRes(whoseMove, res, out var needRes))
            {
                InventResC.BuyRes(whoseMove, res);

                RpcSys.SoundToGeneral(sender, ClipGameTypes.SoundGoldPack);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}
