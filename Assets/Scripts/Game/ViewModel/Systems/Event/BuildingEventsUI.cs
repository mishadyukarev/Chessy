using Chessy.Game.Values;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game.EventsUI.Center
{
    public sealed class BuildingEventsUI : SystemAbstract
    {
        public BuildingEventsUI(in CenterUIEs centerUIEs, in EntitiesModel ents) : base(ents)
        {
            centerUIEs.MarketE.ExitButtonC.AddListener(delegate { Exit(BuildingTypes.Market); });
            centerUIEs.SmelterE.ExitButtonC.AddListener(delegate { Exit(BuildingTypes.Smelter); });
            centerUIEs.SmelterE.ButtonC.AddListener(Melt);

            centerUIEs.MarketE.ButtonUIC(MarketBuyTypes.FoodToWood).AddListener(delegate { Buy(MarketBuyTypes.FoodToWood); });
            centerUIEs.MarketE.ButtonUIC(MarketBuyTypes.WoodToFood).AddListener(delegate { Buy(MarketBuyTypes.WoodToFood); });
            centerUIEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToFood).AddListener(delegate { Buy(MarketBuyTypes.GoldToFood); });
            centerUIEs.MarketE.ButtonUIC(MarketBuyTypes.GoldToWood).AddListener(delegate { Buy(MarketBuyTypes.GoldToWood); });


            //centerUIEs.SmelterE.ButtonC.Button.GetComponent<EventTrigger>().OnDrag(new PointerEventData(default));
        }

        void Exit(in BuildingTypes buildingT)
        {
            E.Sound(ClipTypes.Click).Invoke();
            E.SelectedE.BuildingsC.Set(buildingT, false);
        }

        void Buy(in MarketBuyTypes marketBuyT)
        {
            E.RpcPoolEs.BuyResource_ToMaster(marketBuyT);
        }
        void Melt()
        {
            E.RpcPoolEs.Melt_ToMaster();
        }
        void Tr()
        {

        }


        public void Buy_Master(in MarketBuyTypes marketBuyT, in Player sender)
        {
            var needRes = new Dictionary<ResourceTypes, float>();

            needRes.Add(ResourceTypes.Food, 0);
            needRes.Add(ResourceTypes.Wood, 0);
            needRes.Add(ResourceTypes.Ore, 0);
            needRes.Add(ResourceTypes.Iron, 0);
            needRes.Add(ResourceTypes.Gold, 0);

            switch (marketBuyT)
            {
                case MarketBuyTypes.FoodToWood:
                    needRes[ResourceTypes.Food] = EconomyValues.FOR_BUY_FROM_MARKET_FOOD_TO_WOOD;
                    break;

                case MarketBuyTypes.WoodToFood:
                    needRes[ResourceTypes.Wood] = EconomyValues.FOR_BUY_FROM_MARKET_WOOD_TO_FOOD;
                    break;

                case MarketBuyTypes.GoldToFood:
                    needRes[ResourceTypes.Gold] = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_FOOD;
                    break;

                case MarketBuyTypes.GoldToWood:
                    needRes[ResourceTypes.Gold] = EconomyValues.FOR_BUY_FROM_MARKET_GOLD_TO_WOOD;
                    break;
            }

            var canBuy = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                if (needRes[resT] > E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(resT).Resources -= needRes[resT];
                }
                switch (marketBuyT)
                {
                    case MarketBuyTypes.FoodToWood:
                        E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD;
                        break;

                    case MarketBuyTypes.WoodToFood:
                        E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToFood:
                        E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(ResourceTypes.Food).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD;
                        break;

                    case MarketBuyTypes.GoldToWood:
                        E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(ResourceTypes.Wood).Resources
                            += EconomyValues.AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD;
                        break;
                }

                E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
            }
            else
            {
                E.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }

        public void Melt_Master(in Player sender)
        {
            var needRes = new Dictionary<ResourceTypes, float>();

            needRes.Add(ResourceTypes.Food, 0);
            needRes.Add(ResourceTypes.Wood, EconomyValues.WOOD_NEED_FOR_MELTING);
            needRes.Add(ResourceTypes.Ore, EconomyValues.ORE_NEED_FOR_MELTING);
            needRes.Add(ResourceTypes.Iron, 0);
            needRes.Add(ResourceTypes.Gold, 0);

            var canBuy = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                if (needRes[resT] > E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(resT).Resources)
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(resT).Resources -= needRes[resT];
                }

                E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(ResourceTypes.Iron).Resources += EconomyValues.IRON_AFTER_MELTING;
                E.PlayerInfoE(E.WhoseMove.Player).ResourcesC(ResourceTypes.Gold).Resources += EconomyValues.GOLD_AFTER_MELTING;

                E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
                E.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
            }
        }
    }
}