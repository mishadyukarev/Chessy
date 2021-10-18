using System;

namespace Scripts.Game
{
    internal readonly struct EconomyValues
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

        internal static int AmountResForBuild(BuildingTypes buildingType, ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None: throw new Exception();

                case ResourceTypes.Food:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Wood:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 5;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 5;
                        default: throw new Exception();
                    }
                case ResourceTypes.Ore:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Iron:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Gold:
                    switch (buildingType)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }


        }

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
