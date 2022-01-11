using static Game.Game.EntityLeftCityUIPool;

namespace Game.Game
{
    sealed class LeftCityEventUISys
    {
        internal LeftCityEventUISys()
        {
            Melt<ButtonVC>().AddList(delegate { MeltOre(); });

            Resources<ButtonVC>(ResTypes.Food).AddList(delegate { BuyRes(ResTypes.Food); });
            Resources<ButtonVC>(ResTypes.Wood).AddList(delegate { BuyRes(ResTypes.Wood); });
        }

        void MeltOre()
        {
            if (WhoseMoveC.IsMyMove) EntityPool.Rpc<RpcC>().MeltOreToMaster();
        }

        void BuyRes(ResTypes res)
        {
            if (WhoseMoveC.IsMyMove) EntityPool.Rpc<RpcC>().BuyResToMaster(res);
        }
    }
}