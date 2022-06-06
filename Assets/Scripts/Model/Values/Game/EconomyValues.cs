using System;

namespace Chessy.Game.Values
{
    public static class EconomyValues
    {
        public const float FOOD_FOR_FEEDING_UNITS = 0.1f; //NOT_CHANGE!!!
        public const float WOOD_FOR_BUILDING_FARM = 0.5f;  //NOT_CHANGE!!!




        public const float ADDING_FOOD_AFTER_UPDATE = 0.2f;
        public const float AMOUNT_FOOD_AFTER_KILL_CAMEL = 1f;




        #region Costs


        public const float WOOD_NEED_FOR_MELTING = 1f;
        public const float ORE_NEED_FOR_MELTING = 0.5f;

        public const float IRON_AFTER_MELTING = 4f;
        public const float GOLD_AFTER_MELTING = 1f;


        public const float FOR_BUY_FROM_MARKET_FOOD_TO_WOOD = 1f;
        public const float FOR_BUY_FROM_MARKET_WOOD_TO_FOOD = 0.5f;
        public const float FOR_BUY_FROM_MARKET_GOLD_TO_FOOD = 1f;
        public const float FOR_BUY_FROM_MARKET_GOLD_TO_WOOD = 1f;

        public const float AFTER_BUY_FROM_MARKET_FOOD_TO_WOOD = 0.1f;
        public const float AFTER_BUY_FROM_MARKET_WOOD_TO_FOOD = 0.1f;
        public const float AFTER_BUY_FROM_MARKET_GOLD_TO_FOOD = 1;
        public const float AFTER_BUY_FROM_MARKET_GOLD_TO_WOOD = 0.5f;


        public const float NEED_FOOD_FOR_BUILDING_HOUSE = 0;
        public const float NEED_ORE_FOR_BUILDING_HOUSE = 0;
        public const float NEED_IRON_FOR_BUILDING_HOUSE = 0;
        public const float NEED_GOLD_FOR_BUILDING_HOUSE = 0;

        public const float NEED_FOOD_FOR_BUILDING_MARKET = 0;
        public const float NEED_WOOD_FOR_BUILDING_MARKET = 5f;
        public const float NEED_ORE_FOR_BUILDING_MARKET = 0;
        public const float NEED_IRON_FOR_BUILDING_MARKET = 0;
        public const float NEED_GOLD_FOR_BUILDING_MARKET = 0;

        public const float NEED_FOOD_FOR_BUILDING_SMELTER = 0;
        public const float NEED_WOOD_FOR_BUILDING_SMELTER = 3f;
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

                        case ToolWeaponTypes.Staff:
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
