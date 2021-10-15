using System;

namespace Scripts.Game
{
    internal struct StartEconomyValues
    {
        public const int NULL_RESOURCES = 0;

        public const int AMOUNT_RESOURCES_TYPES = 5;
        public const int FOOD_NUMBER = 0;
        public const int WOOD_NUMBER = 1;
        public const int ORE_NUMBER = 2;
        public const int IRON_NUMBER = 3;
        public const int GOLD_NUMBER = 4;



        internal static int AmountUnits(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return 1;

                case UnitTypes.Pawn:
                    return 1;

                case UnitTypes.Rook:
                    return 0;

                case UnitTypes.Bishop:
                    return 0;

                default:
                    throw new Exception();
            }
        }

        internal static int AmountResources(ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return 25;

                case ResourceTypes.Wood:
                    return 25;

                case ResourceTypes.Ore:
                    return 0;

                case ResourceTypes.Iron:
                    return 0;

                case ResourceTypes.Gold:
                    return 0;

                default:
                    throw new Exception();
            }
        }


        #region Costs


        internal static int AmountResForBuy(UnitTypes unitType, ResourceTypes resourceType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 5;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }

                case UnitTypes.Rook:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 5;
                        case ResourceTypes.Wood: return 5;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }

                case UnitTypes.Bishop:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 5;
                        case ResourceTypes.Wood: return 5;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }

                default:
                    throw new Exception();
            }
        }

        internal static int AmountResForUpgrade(BuildingTypes buildingType, ResourceTypes resourceType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None: throw new Exception();
                case BuildingTypes.City: throw new Exception();
                case BuildingTypes.Farm:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 0;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 3;
                        default: throw new Exception();
                    }
                case BuildingTypes.Woodcutter:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 0;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 3;
                        default: throw new Exception();
                    }
                case BuildingTypes.Mine:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 0;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 3;
                        default: throw new Exception();
                    }

                default: throw new Exception();
            }
        }

        internal static int AmountResForMelting(ResourceTypes resourceTypes)
        {
            switch (resourceTypes)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 5;
                case ResourceTypes.Ore: return 5;
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 0;
                default: throw new Exception();
            }
        }

        #region Fire

        public static int FoodForPawnFire = 0;
        public static int WoodForPawnFire = 0;
        public static int OreForPawnFire = 0;
        public static int IronForPawnFire = 0;
        public static int GoldForPawnFire = 0;

        public static int FoodForPawnSwordFire = 0;
        public static int WoodForPawnSwordFire = 0;
        public static int OreForPawnSwordFire = 0;
        public static int IronForPawnSwordFire = 0;
        public static int GoldForPawnSwordFire = 0;

        public static int FoodForRookFire = 0;
        public static int WoodForRookFire = 0;
        public static int OreForRookFire = 0;
        public static int IronForRookFire = 0;
        public static int GoldForRookFire = 0;

        public static int FoodForRookCrossbowFire = 0;
        public static int WoodForRookCrossbowFire = 0;
        public static int OreForRookCrossbowFire = 0;
        public static int IronForRookCrossbowFire = 0;
        public static int GoldForRookCrossbowFire = 0;

        public static int FoodForBishopFire = 0;
        public static int WoodForBishopFire = 0;
        public static int OreForBishopFire = 0;
        public static int IronForBishopFire = 0;
        public static int GoldForBishopFire = 0;

        public static int FoodForBishopCrossbowFire = 0;
        public static int WoodForBishopCrossbowFire = 0;
        public static int OreForBishopCrossbowFire = 0;
        public static int IronForBishopCrossbowFire = 0;
        public static int GoldForBishopCrossbowFire = 0;


        #endregion


        #region Building

        public const int FOOD_FOR_BUILDING_FARM = 0;
        public const int WOOD_FOR_BUILDING_FARM = 5;
        public const int ORE_FOR_BUILDING_FARM = 0;
        public const int IRON_FOR_BUILDING_FARM = 0;
        public const int GOLD_FOR_BUILDING_FARM = 0;

        public const int FOOD_FOR_BUILDING_MINE = 0;
        public const int WOOD_FOR_BUILDING_MINE = 5;
        public const int ORE_FOR_BUILDING_MINE = 0;
        public const int IRON_FOR_BUILDING_MINE = 0;
        public const int GOLD_FOR_BUILDING_MINE = 0;

        #endregion

        #endregion


        #region Benefit

        public const int BENEFIT_FOOD_FARM = 1;
        public const int BENEFIT_WOOD_WOODCUTTER = 1;
        public const int BENEFIT_ORE_MINE = 1;

        public const int BENEFIT_FOOD_CITY = 1;
        public const int BENEFIT_WOOD_CITY = 1;

        #endregion
    }
}
