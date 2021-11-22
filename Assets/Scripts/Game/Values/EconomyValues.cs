using System;

namespace Game.Game
{
    public readonly struct EconomyValues
    {
        public static int StartAmountUnits(UnitTypes unit, LevelTypes level)
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

        public static int AmountResources(ResTypes resourceType)
        {
            switch (resourceType)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 25;
                case ResTypes.Wood: return 25;
                case ResTypes.Ore:  return 0;
                case ResTypes.Iron: return 0;
                case ResTypes.Gold: return 0;
                default: throw new Exception();
            }
        }


        #region Costs

        public static int AmountResForBuy(UnitTypes unitType, ResTypes resType)
        {
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: throw new Exception();
                case UnitTypes.Pawn:
                    switch (resType)
                    {
                        case ResTypes.None: throw new Exception();
                        case ResTypes.Food: return 5;
                        case ResTypes.Wood: return 0;
                        case ResTypes.Ore: return 0;
                        case ResTypes.Iron: return 0;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                case UnitTypes.Archer:
                    switch (resType)
                    {
                        case ResTypes.None: throw new Exception();
                        case ResTypes.Food: return 5;
                        case ResTypes.Wood: return 5;
                        case ResTypes.Ore: return 0;
                        case ResTypes.Iron: return 0;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        public static int AmountResForBuyRes(ResTypes resTypeBuy)
        {
            switch (resTypeBuy)
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
        public static int AmountResForMelting(ResTypes resourceTypes)
        {
            switch (resourceTypes)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 0;
                case ResTypes.Wood: return 5;
                case ResTypes.Ore: return 5;
                case ResTypes.Iron: return 0;
                case ResTypes.Gold: return 0;
                default: throw new Exception();
            }
        }
        public static int AmountResForBuild(BuildTypes buildingType, ResTypes resourceType)
        {
            switch (resourceType)
            {
                case ResTypes.None: throw new Exception();

                case ResTypes.Food:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResTypes.Wood:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 5;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 5;
                        default: throw new Exception();
                    }
                case ResTypes.Ore:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResTypes.Iron:
                    switch (buildingType)
                    {
                        case BuildTypes.None: throw new Exception();
                        case BuildTypes.City: throw new Exception();
                        case BuildTypes.Farm: return 0;
                        case BuildTypes.Woodcutter: throw new Exception();
                        case BuildTypes.Mine: return 0;
                        default: throw new Exception();
                    }
                case ResTypes.Gold:
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
        public static int AmountResForUpgradeUnit(UnitTypes unitType, ResTypes resourceType)
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
        public static int AmountResForBuyTW(TWTypes toolWeaponType, LevelTypes levelTWType, ResTypes resourceType)
        {
            switch (levelTWType)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First:
                    switch (toolWeaponType)
                    {
                        case TWTypes.None: throw new Exception();
                        case TWTypes.Pick: throw new Exception();
                        case TWTypes.Sword: throw new Exception();
                        case TWTypes.Shield:
                            switch (resourceType)
                            {
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 1;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 0;
                                case ResTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        default: throw new Exception();
                    }
                case LevelTypes.Second:
                    switch (toolWeaponType)
                    {
                        case TWTypes.None: throw new Exception();
                        case TWTypes.Pick:
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
                        case TWTypes.Sword:
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
                        case TWTypes.Shield:
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
                default: throw new Exception();
            }

            
        }

        #endregion


        #region Benefit


        #endregion
    }
}
