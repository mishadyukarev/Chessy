using Game.Common;
using static Game.Game.EntityLeftCityUIPool;

namespace Game.Game
{
    sealed class LeftCityEventUISys
    {
        internal LeftCityEventUISys()
        {
            Melt<ButtonUIC>().AddListener(delegate { MeltOre(); });

            Resources<ButtonUIC>(ResourceTypes.Food).AddListener(delegate { BuyRes(ResourceTypes.Food); });
            Resources<ButtonUIC>(ResourceTypes.Wood).AddListener(delegate { BuyRes(ResourceTypes.Wood); });
        }

        void MeltOre()
        {
            if (Entities.WhoseMoveE.IsMyMove) EntityPool.Rpc.MeltOreToMaster();
        }

        void BuyRes(ResourceTypes res)
        {
            if (Entities.WhoseMoveE.IsMyMove) EntityPool.Rpc.BuyResToMaster(res);
        }
    }
}