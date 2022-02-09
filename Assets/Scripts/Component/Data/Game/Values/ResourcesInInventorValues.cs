using System;

namespace Game.Game
{
    public static class ResourcesInInventorValues
    {
        public const int ADDING_FOOD_AFTER_MOVE = 30;
        public const int AMOUNT_FOOD_AFTER_KILL_CAMEL = 50;

        internal static int AmountResourcesOnStartGame(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 250;
                case ResourceTypes.Wood: return 250;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 5;
                case ResourceTypes.Gold: return 5;
                default: throw new Exception();
            }
        }
        public static int CostFoodForFeedingThem(in UnitTypes unit)
        {
            if (unit != UnitTypes.King) return 10;
            else return 0;
        }


        #region Costs

        internal static int ForBuy(in UnitTypes unit, in ResourceTypes res)
        {
            switch (unit)
            {
                case UnitTypes.Pawn:
                    switch (res)
                    {
                        case ResourceTypes.Food: return 50;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuyRes(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 0;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 1;
                default: throw new Exception();
            }
        }
        internal static int ForMelting(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 50;
                case ResourceTypes.Ore: return 50;
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 0;
                default: throw new Exception();
            }
        }
        internal static int AfterMelting(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 0;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 5;
                case ResourceTypes.Gold: return 1;
                default: throw new Exception();
            }
        }
        internal static int ForBuild(in BuildingTypes build, in ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.Food:
                    switch (build)
                    {
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.House: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Wood:
                    switch (build)
                    {
                        case BuildingTypes.Farm: return 50;
                        case BuildingTypes.House: return 50;
                        default: throw new Exception();
                    }
                case ResourceTypes.Ore:
                    switch (build)
                    {
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.House: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Iron:
                    switch (build)
                    {
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.House: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Gold:
                    switch (build)
                    {
                        case BuildingTypes.Farm: return 0;
                            case BuildingTypes.House: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }


        }
        internal static int AmountResForUpgradeUnit(in UnitTypes unitType, in ResourceTypes resourceType)
        {
            switch (unitType)
            {
                case UnitTypes.Pawn:
                    switch (resourceType)
                    {
                        case ResourceTypes.Food: return 0;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 1;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuyTW(in ToolWeaponTypes tW, in LevelTypes level, in ResourceTypes res)
        {
            switch (level)
            {
                case LevelTypes.First:
                    switch (tW)
                    {
                        case ToolWeaponTypes.Shield:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 10;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 0;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.BowCrossbow:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 50;
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
                        case ToolWeaponTypes.Pick:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 30;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 0;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
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
                                case ResourceTypes.Iron: return 1;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.BowCrossbow:
                            switch (res)
                            {
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 1;
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
