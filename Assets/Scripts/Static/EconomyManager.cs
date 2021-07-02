using Assets.Scripts.Abstractions;
using Photon.Realtime;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public static class EconomyManager
    {

        internal static bool CanCreateBuilding(BuildingTypes buildingType, Player player, out bool[] haves)
        {
            haves = new bool[ValuesConst.AMOUNT_RESOURCES_TYPES];
            for (int i = 0; i < haves.Length; i++) haves[i] = true;

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return true;

                case BuildingTypes.Farm:
                    haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                    haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                    haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                    haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                    haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                    break;

                case BuildingTypes.Mine:
                    haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                    haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                    haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                    haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                    haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                    break;

                default:
                    break;
            }

            if (haves[ValuesConst.FOOD_NUMBER] && haves[ValuesConst.WOOD_NUMBER] && haves[ValuesConst.ORE_NUMBER] && haves[ValuesConst.IRON_NUMBER] && haves[ValuesConst.GOLD_NUMBER]) return true;
            else return false;
        }

        internal static void CreateBuilding(BuildingTypes buildingType, Player player)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    break;

                case BuildingTypes.Farm:
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUILDING_FARM);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUILDING_FARM);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM);
                    break;

                case BuildingTypes.Woodcutter:
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER);
                    break;

                case BuildingTypes.Mine:
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUILDING_MINE);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUILDING_MINE);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE);
                    break;

                default:
                    break;
            }
        }


        internal static bool CanCreateUnit(UnitTypes unitType, Player player, out bool[] haves)
        {
            haves = new bool[ValuesConst.AMOUNT_RESOURCES_TYPES];
            for (int i = 0; i < haves.Length; i++) haves[i] = true;

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;
                    break;

                default:
                    break;
            }

            if (haves[ValuesConst.FOOD_NUMBER] && haves[ValuesConst.WOOD_NUMBER] && haves[ValuesConst.ORE_NUMBER] && haves[ValuesConst.IRON_NUMBER] && haves[ValuesConst.GOLD_NUMBER]) return true;
            else return false;
        }

        internal static void CreateUnit(UnitTypes unitType, Player player)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUYING_PAWN);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUYING_PAWN);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUYING_PAWN);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUYING_PAWN);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUYING_PAWN);
                    break;

                case UnitTypes.Rook:
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUYING_ROOK);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUYING_ROOK);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUYING_ROOK);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUYING_ROOK);
                    break;

                case UnitTypes.Bishop:
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUYING_BISHOP);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP);
                    Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP);
                    break;

                default:
                    break;
            }

            Instance.EGM.UnitInventorEnt_UnitInventorCom.AddAmountUnits(unitType, player.IsMasterClient);
        }


        internal static bool CanMeltOre(Player player, out bool[] haves)
        {
            haves = new bool[ValuesConst.AMOUNT_RESOURCES_TYPES];
            for (int i = 0; i < haves.Length; i++) haves[i] = true;

            haves[ValuesConst.FOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haves[ValuesConst.WOOD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haves[ValuesConst.ORE_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haves[ValuesConst.IRON_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haves[ValuesConst.GOLD_NUMBER] = Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haves[ValuesConst.FOOD_NUMBER] && haves[ValuesConst.WOOD_NUMBER] && haves[ValuesConst.ORE_NUMBER] && haves[ValuesConst.IRON_NUMBER] && haves[ValuesConst.GOLD_NUMBER]) return true;
            else return false;
        }

        internal static void MeltOre(Player player)
        {
            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Food, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_MELTING_ORE);
            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Wood, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_MELTING_ORE);
            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Ore, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_MELTING_ORE);
            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_MELTING_ORE);
            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_MELTING_ORE);

            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Iron, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) + 1);
            Instance.EGM.EconomyEnt_EconomyCom.SetAmountResources(ResourceTypes.Gold, player.IsMasterClient, Instance.EGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) + 1);
        }
    }
}