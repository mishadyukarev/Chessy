using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class LeftCityEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            CutyLeftZoneViewUIC.AddListenerToMelt(delegate { MeltOre(); });

            CutyLeftZoneViewUIC.AddListToBuyRes(ResTypes.Food, delegate { BuyRes(ResTypes.Food); });
            CutyLeftZoneViewUIC.AddListToBuyRes(ResTypes.Wood, delegate { BuyRes(ResTypes.Wood); });
        }

        private void MeltOre()
        {
            if (WhoseMoveC.IsMyMove) RpcSys.MeltOreToMaster();
        }

        private void BuyRes(ResTypes res)
        {
            if (WhoseMoveC.IsMyMove) RpcSys.BuyResToMaster(res);
        }
    }
}