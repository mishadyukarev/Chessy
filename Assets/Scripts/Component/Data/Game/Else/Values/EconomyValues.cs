using System;

namespace Game.Game
{
    public static class EconomyValues
    {
        public const int ADDING_FOOD_AFTER_MOVE = 30;
        public static int CostFood(in UnitTypes unit)
        {
            if (unit != UnitTypes.King) return 10;
            else return 0;
        }

        internal static int StartAmountUnits(in UnitTypes unit, in LevelTypes level)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Pawn:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer: return 0;
                case UnitTypes.Scout:
                    switch (level)
                    {
                        case LevelTypes.None: throw new Exception();
                        case LevelTypes.First: return 1;
                        case LevelTypes.Second: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Elfemale: return 0;
                default: throw new Exception();
            }
        }

        internal static int AmountResources(ResourceTypes res)
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


        #region Costs

        internal static int AmountResForBuy(UnitTypes unit, ResourceTypes res)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn:
                    switch (res)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 50;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer:
                    switch (res)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 50;
                        case ResourceTypes.Wood: return 50;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuyRes(ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 0;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 1;
                default: throw new Exception();
            }
        }
        internal static int AmountResForMelting(ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 50;
                case ResourceTypes.Ore: return 50;
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 0;
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuild(BuildingTypes build, ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None: throw new Exception();

                case ResourceTypes.Food:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Wood:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 50;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 50;
                        default: throw new Exception();
                    }
                case ResourceTypes.Ore:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Iron:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Gold:
                    switch (build)
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
        internal static int AmountResForUpgradeUnit(UnitTypes unitType, ResourceTypes resourceType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 0;
                        case ResourceTypes.Wood: return 0;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 1;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer:
                    switch (resourceType)
                    {
                        case ResourceTypes.None: throw new Exception();
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
        internal static int AmountResForBuyTW(ToolWeaponTypes tW, LevelTypes level, ResourceTypes res)
        {
            switch (level)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First:
                    switch (tW)
                    {
                        case ToolWeaponTypes.None: throw new Exception();
                        case ToolWeaponTypes.Pick: throw new Exception();
                        case ToolWeaponTypes.Sword: throw new Exception();
                        case ToolWeaponTypes.Shield:
                            switch (res)
                            {
                                case ResourceTypes.None: throw new Exception();
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 10;
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
                        case ToolWeaponTypes.None: throw new Exception();
                        case ToolWeaponTypes.Pick:
                            switch (res)
                            {
                                case ResourceTypes.None: throw new Exception();
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 0;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 1;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.Sword:
                            switch (res)
                            {
                                case ResourceTypes.None: throw new Exception();
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
                                case ResourceTypes.None: throw new Exception();
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
