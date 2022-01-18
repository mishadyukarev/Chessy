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

        internal static int AmountResources(ResTypes res)
        {
            switch (res)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 250;
                case ResTypes.Wood: return 250;
                case ResTypes.Ore: return 0;
                case ResTypes.Iron: return 5;
                case ResTypes.Gold: return 5;
                default: throw new Exception();
            }
        }


        #region Costs

        internal static int AmountResForBuy(UnitTypes unit, ResTypes res)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn:
                    switch (res)
                    {
                        case ResTypes.None: throw new Exception();
                        case ResTypes.Food: return 50;
                        case ResTypes.Wood: return 0;
                        case ResTypes.Ore: return 0;
                        case ResTypes.Iron: return 0;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer:
                    switch (res)
                    {
                        case ResTypes.None: throw new Exception();
                        case ResTypes.Food: return 50;
                        case ResTypes.Wood: return 50;
                        case ResTypes.Ore: return 0;
                        case ResTypes.Iron: return 0;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuyRes(ResTypes res)
        {
            switch (res)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 0;
                case ResTypes.Wood: return 0;
                case ResTypes.Ore: return 0;
                case ResTypes.Iron: return 0;
                case ResTypes.Gold: return 1;
                default: throw new Exception();
            }
        }
        internal static int AmountResForMelting(ResTypes res)
        {
            switch (res)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 0;
                case ResTypes.Wood: return 50;
                case ResTypes.Ore: return 50;
                case ResTypes.Iron: return 0;
                case ResTypes.Gold: return 0;
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuild(BuildingTypes build, ResTypes resourceType)
        {
            switch (resourceType)
            {
                case ResTypes.None: throw new Exception();

                case ResTypes.Food:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResTypes.Wood:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 50;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 50;
                        default: throw new Exception();
                    }
                case ResTypes.Ore:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResTypes.Iron:
                    switch (build)
                    {
                        case BuildingTypes.None: throw new Exception();
                        case BuildingTypes.City: throw new Exception();
                        case BuildingTypes.Farm: return 0;
                        case BuildingTypes.Woodcutter: throw new Exception();
                        case BuildingTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResTypes.Gold:
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
        internal static int AmountResForUpgradeUnit(UnitTypes unitType, ResTypes resourceType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn:
                    switch (resourceType)
                    {
                        case ResTypes.None: throw new Exception();
                        case ResTypes.Food: return 0;
                        case ResTypes.Wood: return 0;
                        case ResTypes.Ore: return 0;
                        case ResTypes.Iron: return 1;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer:
                    switch (resourceType)
                    {
                        case ResTypes.None: throw new Exception();
                        case ResTypes.Food: return 0;
                        case ResTypes.Wood: return 0;
                        case ResTypes.Ore: return 0;
                        case ResTypes.Iron: return 1;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal static int AmountResForBuyTW(ToolWeaponTypes tW, LevelTypes level, ResTypes res)
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
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 10;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 0;
                                case ResTypes.Gold: return 0;
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
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 0;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 1;
                                case ResTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.Sword:
                            switch (res)
                            {
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 0;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 2;
                                case ResTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case ToolWeaponTypes.Shield:
                            switch (res)
                            {
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 0;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 3;
                                case ResTypes.Gold: return 0;
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
