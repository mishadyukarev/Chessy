using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public class BuyResMastS : IEcsRunSystem
    {
        public void Run()
        {
            var res = ForBuyResMasC.Res;

            var sender = InfoC.Sender(MGOTypes.Master);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (InvResC.CanBuy(whoseMove, res, out var needRes))
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
