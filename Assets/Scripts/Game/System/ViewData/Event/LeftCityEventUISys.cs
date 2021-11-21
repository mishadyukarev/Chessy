using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class LeftCityEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            CutyLeftUIC.AddListenerToMelt(delegate { MeltOre(); });

            CutyLeftUIC.AddListToBuyRes(ResTypes.Food, delegate { BuyRes(ResTypes.Food); });
            CutyLeftUIC.AddListToBuyRes(ResTypes.Wood, delegate { BuyRes(ResTypes.Wood); });
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