using static Game.Game.EntityLeftCityUIPool;

namespace Game.Game
{
    sealed class LeftCityEventUISys : SystemViewAbstract
    {
        public LeftCityEventUISys(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            Melt<ButtonUIC>().AddListener(delegate { MeltOre(); });

            Resources<ButtonUIC>(ResourceTypes.Food).AddListener(delegate { BuyRes(ResourceTypes.Food); });
            Resources<ButtonUIC>(ResourceTypes.Wood).AddListener(delegate { BuyRes(ResourceTypes.Wood); });
        }

        void MeltOre()
        {
            if (Es.WhoseMoveE.IsMyMove) Es.RpcE.MeltOreToMaster();
        }

        void BuyRes(ResourceTypes res)
        {
            if (Es.WhoseMoveE.IsMyMove) Es.RpcE.BuyResToMaster(res);
        }
    }
}