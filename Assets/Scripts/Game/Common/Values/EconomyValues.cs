using System;

namespace Scripts.Game
{
    public readonly struct EconomyValues
    {
        public static int AmountUnits(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 1;
                case UnitTypes.Pawn: return 1;
                case UnitTypes.Rook: return 0;
                case UnitTypes.Bishop: return 0;
                case UnitTypes.Scout: return 1;
                default: throw new Exception();
            }
        }

        public static int AmountResources(ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 25;
                case ResourceTypes.Wood: return 25;
                case ResourceTypes.Ore:  return 0;
                case ResourceTypes.Iron: return 5;
                case ResourceTypes.Gold: return 0;
                default: throw new Exception();
            }
        }


        #region Costs

        public static int AmountResForBuy(UnitTypes unitType, ResourceTypes resType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn:
                    switch (resType)
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
                    switch (resType)
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
                    switch (resType)
                    {
                        case ResourceTypes.None: throw new Exception();
                        case ResourceTypes.Food: return 5;
                        case ResourceTypes.Wood: return 5;
                        case ResourceTypes.Ore: return 0;
                        case ResourceTypes.Iron: return 0;
                        case ResourceTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        public static int AmountResForUpgrade(BuildTypes buildingType, ResourceTypes resourceType)
        {
            switch (buildingType)
            {
                case BuildTypes.None: throw new Exception();
                case BuildTypes.City: throw new Exception();
                case BuildTypes.Farm:
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
                case BuildTypes.Woodcutter:
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
                case BuildTypes.Mine:
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
        public static int AmountResForMelting(ResourceTypes resourceTypes)
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
        public static int AmountResForBuild(BuildTypes buildingType, ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None: throw new Exception();

                case ResourceTypes.Food:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Wood:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 5;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 5;
                        default: throw new Exception();
                    }
                case ResourceTypes.Ore:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Iron:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResourceTypes.Gold:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }


        }
        public static int AmountResForUpgradeUnit(UnitTypes unitType, ResourceTypes resourceType)
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
                case UnitTypes.Rook:
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
                case UnitTypes.Bishop:
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
        public static int AmountResForBuyTW(ToolWeaponTypes toolWeaponType, LevelTWTypes levelTWType, ResourceTypes resourceType)
        {
            switch (levelTWType)
            {
                case LevelTWTypes.None: throw new Exception();
                case LevelTWTypes.Wood:
                    switch (toolWeaponType)
                    {
                        case ToolWeaponTypes.None: throw new Exception();
                        case ToolWeaponTypes.Hoe: throw new Exception();
                        case ToolWeaponTypes.Pick: throw new Exception();
                        case ToolWeaponTypes.Sword: throw new Exception();
                        case ToolWeaponTypes.Shield:
                            switch (resourceType)
                            {
                                case ResourceTypes.None: throw new Exception();
                                case ResourceTypes.Food: return 0;
                                case ResourceTypes.Wood: return 1;
                                case ResourceTypes.Ore: return 0;
                                case ResourceTypes.Iron: return 0;
                                case ResourceTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        default: throw new Exception();
                    }
                case LevelTWTypes.Iron:
                    switch (toolWeaponType)
                    {
                        case ToolWeaponTypes.None: throw new Exception();
                        case ToolWeaponTypes.Hoe: throw new Exception();
                        case ToolWeaponTypes.Pick:
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
                        case ToolWeaponTypes.Sword:
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
                        case ToolWeaponTypes.Shield:
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
                default: throw new Exception();
            }

            
        }

        #endregion


        #region Benefit


        #endregion
    }
}
