using Assets.Scripts.Workers.Cell;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.EconomyValues;

namespace Assets.Scripts.Workers.Game.Else.Economy
{
    internal sealed class InfoExtractionWorker : MainGeneralWorker
    {
        internal static int GetExtractionOneBuilding(BuildingTypes buildingType, int amountUpgrades)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    return BENEFIT_FOOD_FARM + BENEFIT_FOOD_FARM * amountUpgrades;

                case BuildingTypes.Woodcutter:
                    return BENEFIT_WOOD_WOODCUTTER + BENEFIT_WOOD_WOODCUTTER * amountUpgrades;

                case BuildingTypes.Mine:
                    return BENEFIT_ORE_MINE + BENEFIT_ORE_MINE * amountUpgrades;

                default:
                    throw new Exception();
            }
        }
    }
}
