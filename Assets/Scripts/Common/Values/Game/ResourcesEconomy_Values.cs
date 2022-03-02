using System;

namespace Chessy.Game
{
    public static class ResourcesEconomy_Values
    {
        public const float ADDING_FOOD_AFTER_MOVE = 0.3f;
        public const float AMOUNT_FOOD_AFTER_KILL_CAMEL = 1f;

        public static float CostFoodForFeedingThem(in UnitTypes unit)
        {
            if (unit != UnitTypes.King) return 0.1f;
            else return 0;
        }


        #region Costs

        public static float ResourcesForBuyFromMarket(in MarketBuyTypes marketT)
        {
            switch (marketT)
            {
                case MarketBuyTypes.FoodToWood: return 1;
                case MarketBuyTypes.WoodToFood: return 0.5f;
                case MarketBuyTypes.GoldToFood: return 1;
                case MarketBuyTypes.GoldToWood: return 1;
                default: throw new Exception();
            }
        }
        public static float ResourcesAfterBuyInMarket(in MarketBuyTypes marketT)
        {
            switch (marketT)
            {
                case MarketBuyTypes.FoodToWood: return 0.1f;
                case MarketBuyTypes.WoodToFood: return 0.1f;
                case MarketBuyTypes.GoldToFood: return 1;
                case MarketBuyTypes.GoldToWood: return 0.5f;
                default: throw new Exception();
            }
        }
        public static float AfterMelting(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 0;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 0.05f;
                case ResourceTypes.Gold: return 0.01f;
                default: throw new Exception();
            }
        }


        public const float NEED_FOOD_FOR_BUILDING_HOUSE = 0;
        public const float NEED_WOOD_FOR_BUILDING_HOUSE = 1f;
        public const float NEED_ORE_FOR_BUILDING_HOUSE = 0;
        public const float NEED_IRON_FOR_BUILDING_HOUSE = 0;
        public const float NEED_GOLD_FOR_BUILDING_HOUSE = 0;

        public const float NEED_FOOD_FOR_BUILDING_MARKET = 0;
        public const float NEED_WOOD_FOR_BUILDING_MARKET = 10f;
        public const float NEED_ORE_FOR_BUILDING_MARKET = 0;
        public const float NEED_IRON_FOR_BUILDING_MARKET = 0;
        public const float NEED_GOLD_FOR_BUILDING_MARKET = 0;

        public const float NEED_FOOD_FOR_BUILDING_SMELTER = 0;
        public const float NEED_WOOD_FOR_BUILDING_SMELTER = 5f;
        public const float NEED_ORE_FOR_BUILDING_SMELTER = 0;
        public const float NEED_IRON_FOR_BUILDING_SMELTER = 0;
        public const float NEED_GOLD_FOR_BUILDING_SMELTER = 0;

        public static float ForBuyToolWeapon(in ToolWeaponTypes tW, in LevelTypes level, in ResourceTypes res)
        {
            switch (level)
            {
                case LevelTypes.First:
                    switch (tW)
                    {
                        case ToolWeaponTypes.Pick:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0.3f;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 0;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.Shield:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0.1f;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 0;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.BowCrossbow:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0.5f;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 0;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        default: throw new Exception();
                    }
                case LevelTypes.Second:
                    switch (tW)
                    {
                        case ToolWeaponTypes.Sword:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 1;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.Shield:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 1f;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.BowCrossbow:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 1f;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.Axe:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 1f;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }


        }

        #endregion
    }
}
