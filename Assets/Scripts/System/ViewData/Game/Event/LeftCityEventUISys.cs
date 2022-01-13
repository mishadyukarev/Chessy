using Game.Common;
using static Game.Game.EntityLeftCityUIPool;

namespace Game.Game
{
    sealed class LeftCityEventUISys
    {
        internal LeftCityEventUISys()
        {
            Melt<ButtonUIC>().AddListener(delegate { MeltOre(); });

            Resources<ButtonUIC>(ResTypes.Food).AddListener(delegate { BuyRes(ResTypes.Food); });
            Resources<ButtonUIC>(ResTypes.Wood).AddListener(delegate { BuyRes(ResTypes.Wood); });
        }

        void MeltOre()
        {
            if (EntWhoseMove.IsMyMove) EntityPool.Rpc<RpcC>().MeltOreToMaster();
        }

        void BuyRes(ResTypes res)
        {
            if (EntWhoseMove.IsMyMove) EntityPool.Rpc<RpcC>().BuyResToMaster(res);
        }
    }
}