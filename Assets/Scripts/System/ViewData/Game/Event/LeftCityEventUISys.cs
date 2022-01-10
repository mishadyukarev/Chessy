

namespace Game.Game
{
    sealed class LeftCityEventUISys
    {
        internal LeftCityEventUISys()
        {
            CutyLeftUIC.AddListenerToMelt(delegate { MeltOre(); });

            CutyLeftUIC.AddListToBuyRes(ResTypes.Food, delegate { BuyRes(ResTypes.Food); });
            CutyLeftUIC.AddListToBuyRes(ResTypes.Wood, delegate { BuyRes(ResTypes.Wood); });
        }

        private void MeltOre()
        {
            if (WhoseMoveC.IsMyMove) EntityPool.Rpc<RpcC>().MeltOreToMaster();
        }

        private void BuyRes(ResTypes res)
        {
            if (WhoseMoveC.IsMyMove) EntityPool.Rpc<RpcC>().BuyResToMaster(res);
        }
    }
}