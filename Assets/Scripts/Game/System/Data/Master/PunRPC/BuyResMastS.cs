using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public class BuyResMastS : IEcsRunSystem
    {
        public void Run()
        {
            var res = ForBuyResMasC.Res;

            var sender = InfoC.Sender(MGOTypes.Master);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (InvResC.CanBuyRes(whoseMove, res, out var needRes))
            {
                InvResC.BuyRes(whoseMove, res);

                RpcSys.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                RpcSys.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}
