using System;

namespace Game.Game
{
    internal readonly struct EconomyValues
    {
        internal int Adding(ResTypes res)
        {
            switch (res)
            {
                case ResTypes.Food: return 40;
                case ResTypes.Wood: return 0;
                case ResTypes.Ore: return 5;
                default: return 0;
            }
        }

        internal int StartAmountUnits(UnitTypes unit, LevelTypes level)
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

        internal int AmountResources(ResTypes resourceType)
        {
            switch (resourceType)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 250;
                case ResTypes.Wood: return 250;
                case ResTypes.Ore: return 0;
                case ResTypes.Iron: return 0;
                case ResTypes.Gold: return 0;
                default: throw new Exception();
            }
        }


        #region Costs

        internal int AmountResForBuy(UnitTypes unit, ResTypes res)
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
        internal int AmountResForBuyRes(ResTypes res)
        {
            switch (res)
            {
                case ResTypes.None: throw new Exception();
                case ResTypes.Food: return 0;
                case ResTypes.Wood: return 0;
                case ResTypes.Ore: return 0;
                case ResTypes.Iron: return 0;
                case ResTypes.Gold: return 10;
                default: throw new Exception();
            }
        }
        internal int AmountResForMelting(ResTypes res)
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
        internal int AmountResForBuild(BuildingTypes build, ResTypes resourceType)
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
        internal int AmountResForUpgradeUnit(UnitTypes unitType, ResTypes resourceType)
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
                        case ResTypes.Iron: return 10;
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
                        case ResTypes.Iron: return 10;
                        case ResTypes.Gold: return 0;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }
        internal int AmountResForBuyTW(TWTypes tW, LevelTypes level, ResTypes res)
        {
            switch (level)
            {
                case LevelTypes.None: throw new Exception();
                case LevelTypes.First:
                    switch (tW)
                    {
                        case TWTypes.None: throw new Exception();
                        case TWTypes.Pick: throw new Exception();
                        case TWTypes.Sword: throw new Exception();
                        case TWTypes.Shield:
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
                        case TWTypes.None: throw new Exception();
                        case TWTypes.Pick:
                            switch (res)
                            {
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 0;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 10;
                                case ResTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case TWTypes.Sword:
                            switch (res)
                            {
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 0;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 10;
                                case ResTypes.Gold: return 0;
                                default: throw new Exception();
                            }
                        case TWTypes.Shield:
                            switch (res)
                            {
                                case ResTypes.None: throw new Exception();
                                case ResTypes.Food: return 0;
                                case ResTypes.Wood: return 0;
                                case ResTypes.Ore: return 0;
                                case ResTypes.Iron: return 10;
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
