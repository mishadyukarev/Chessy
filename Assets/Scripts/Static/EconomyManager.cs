using Photon.Realtime;
using System;
using static Assets.Scripts.Abstractions.ValuesConst;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public static class EconomyManager
    {
        private static StartGameValuesConfig StartVGC => Instance.StartValuesGameConfig;

        internal static bool CanCreateBuilding(BuildingTypes buildingType, Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return true;

                case BuildingTypes.Farm:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_BUILDING_FARM;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_BUILDING_FARM;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_BUILDING_FARM;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_BUILDING_FARM;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_BUILDING_FARM;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_BUILDING_WOODCUTTER == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_BUILDING_WOODCUTTER;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_BUILDING_WOODCUTTER == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_BUILDING_WOODCUTTER;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_BUILDING_WOODCUTTER == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_BUILDING_WOODCUTTER;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_BUILDING_WOODCUTTER == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_BUILDING_WOODCUTTER;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_BUILDING_WOODCUTTER == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_BUILDING_WOODCUTTER;
                    break;

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_BUILDING_MINE;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_BUILDING_MINE;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_BUILDING_MINE;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_BUILDING_MINE;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_BUILDING_MINE;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
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
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_BUILDING_FARM);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_BUILDING_FARM);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_BUILDING_FARM);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_BUILDING_FARM);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_BUILDING_FARM);
                    break;

                case BuildingTypes.Woodcutter:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_BUILDING_WOODCUTTER);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_BUILDING_WOODCUTTER);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_BUILDING_WOODCUTTER);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_BUILDING_WOODCUTTER);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_BUILDING_WOODCUTTER);
                    break;

                case BuildingTypes.Mine:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_BUILDING_MINE);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_BUILDING_MINE);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_BUILDING_MINE);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_BUILDING_MINE);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_BUILDING_MINE);
                    break;

                default:
                    break;
            }
        }


        internal static bool CanCreateUnit(UnitTypes unitType, Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_BUYING_PAWN;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_BUYING_PAWN;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_BUYING_PAWN;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_BUYING_PAWN;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_BUYING_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_BUYING_ROOK;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_BUYING_ROOK;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_BUYING_ROOK;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_BUYING_ROOK;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_BUYING_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_BUYING_BISHOP;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_BUYING_BISHOP;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_BUYING_BISHOP;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_BUYING_BISHOP;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_BUYING_BISHOP;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
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
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_BUYING_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_BUYING_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_BUYING_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_BUYING_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_BUYING_PAWN);
                    break;

                case UnitTypes.Rook:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_BUYING_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_BUYING_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_BUYING_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_BUYING_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_BUYING_ROOK);
                    break;

                case UnitTypes.Bishop:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_BUYING_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_BUYING_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_BUYING_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_BUYING_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_BUYING_BISHOP);
                    break;

                default:
                    break;
            }
        }


        internal static bool CanMeltOre(Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_MELTING_ORE;
            haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_MELTING_ORE;
            haves[ORE_NUMBER] = StartVGC.ORE_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_MELTING_ORE;
            haves[IRON_NUMBER] = StartVGC.IRON_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_MELTING_ORE;
            haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_MELTING_ORE;


            return HavedAll(haves);
        }

        internal static void MeltOre(Player player)
        {
            Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_MELTING_ORE);
            Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_MELTING_ORE);
            Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_MELTING_ORE);
            Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_MELTING_ORE);
            Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_MELTING_ORE);

            Instance.EGGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Iron, player.IsMasterClient, 3);
            Instance.EGGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Gold, player.IsMasterClient, 1);
        }


        internal static bool CanFireSomething(Player player, UnitTypes unitType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = StartVGC.FoodForPawnFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForPawnFire;
                    haves[WOOD_NUMBER] = StartVGC.WoodForPawnFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForPawnFire;
                    haves[ORE_NUMBER] = StartVGC.OreForPawnFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForPawnFire;
                    haves[IRON_NUMBER] = StartVGC.IronForPawnFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForPawnFire;
                    haves[GOLD_NUMBER] = StartVGC.GoldForPawnFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForPawnFire;
                    break;

                case UnitTypes.PawnSword:
                    haves[FOOD_NUMBER] = StartVGC.FoodForPawnSwordFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForPawnSwordFire;
                    haves[WOOD_NUMBER] = StartVGC.WoodForPawnSwordFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForPawnSwordFire;
                    haves[ORE_NUMBER] = StartVGC.OreForPawnSwordFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForPawnSwordFire;
                    haves[IRON_NUMBER] = StartVGC.IronForPawnSwordFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForPawnSwordFire;
                    haves[GOLD_NUMBER] = StartVGC.GoldForPawnSwordFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForPawnSwordFire;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = StartVGC.FoodForRookFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForRookFire;
                    haves[WOOD_NUMBER] = StartVGC.WoodForRookFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForRookFire;
                    haves[ORE_NUMBER] = StartVGC.OreForRookFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForRookFire;
                    haves[IRON_NUMBER] = StartVGC.IronForRookFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForRookFire;
                    haves[GOLD_NUMBER] = StartVGC.GoldForRookFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForRookFire;
                    break;

                case UnitTypes.RookCrossbow:
                    haves[FOOD_NUMBER] = StartVGC.FoodForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForRookCrossbowFire;
                    haves[WOOD_NUMBER] = StartVGC.WoodForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForRookCrossbowFire;
                    haves[ORE_NUMBER] = StartVGC.OreForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForRookCrossbowFire;
                    haves[IRON_NUMBER] = StartVGC.IronForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForRookCrossbowFire;
                    haves[GOLD_NUMBER] = StartVGC.GoldForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForRookCrossbowFire;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = StartVGC.FoodForBishopFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForBishopFire;
                    haves[WOOD_NUMBER] = StartVGC.WoodForBishopFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForBishopFire;
                    haves[ORE_NUMBER] = StartVGC.OreForBishopFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForBishopFire;
                    haves[IRON_NUMBER] = StartVGC.IronForBishopFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForBishopFire;
                    haves[GOLD_NUMBER] = StartVGC.GoldForBishopFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForBishopFire;
                    break;

                case UnitTypes.BishopCrossbow:
                    haves[FOOD_NUMBER] = StartVGC.FoodForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForBishopCrossbowFire;
                    haves[WOOD_NUMBER] = StartVGC.WoodForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForBishopCrossbowFire;
                    haves[ORE_NUMBER] = StartVGC.OreForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForBishopCrossbowFire;
                    haves[IRON_NUMBER] = StartVGC.IronForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForBishopCrossbowFire;
                    haves[GOLD_NUMBER] = StartVGC.GoldForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForBishopCrossbowFire;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
        }

        internal static void Fire(Player player, UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FoodForPawnFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WoodForPawnFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.OreForPawnFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IronForPawnFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GoldForPawnFire);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FoodForPawnSwordFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WoodForPawnSwordFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.OreForPawnSwordFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IronForPawnSwordFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GoldForPawnSwordFire);
                    break;

                case UnitTypes.Rook:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FoodForRookFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WoodForRookFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.OreForRookFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IronForRookFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GoldForRookFire);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FoodForRookCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WoodForRookCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.OreForRookCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IronForRookCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GoldForRookCrossbowFire);
                    break;

                case UnitTypes.Bishop:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FoodForBishopFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WoodForBishopFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.OreForBishopFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IronForBishopFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GoldForBishopFire);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FoodForBishopCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WoodForBishopCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.OreForBishopCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IronForBishopCrossbowFire);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GoldForBishopCrossbowFire);
                    break;

                default:
                    break;
            }
        }

        internal static bool CanUpgradeUnit(Player player, UnitTypes unitType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_UPGRADE_PAWN;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_UPGRADE_PAWN;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_UPGRADE_PAWN;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_UPGRADE_PAWN;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_UPGRADE_PAWN;
                    break;

                case UnitTypes.PawnSword:
                    throw new Exception();

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_UPGRADE_ROOK;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_UPGRADE_ROOK;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_UPGRADE_ROOK;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_UPGRADE_ROOK;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_UPGRADE_ROOK;
                    break;

                case UnitTypes.RookCrossbow:
                    throw new Exception();

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = StartVGC.FOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FOOD_FOR_UPGRADE_BISHOP;
                    haves[WOOD_NUMBER] = StartVGC.WOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WOOD_FOR_UPGRADE_BISHOP;
                    haves[ORE_NUMBER] = StartVGC.ORE_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.ORE_FOR_UPGRADE_BISHOP;
                    haves[IRON_NUMBER] = StartVGC.IRON_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IRON_FOR_UPGRADE_BISHOP;
                    haves[GOLD_NUMBER] = StartVGC.GOLD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GOLD_FOR_UPGRADE_BISHOP;
                    break;

                case UnitTypes.BishopCrossbow:
                    throw new Exception();

                default:
                    throw new Exception();
            }

            return HavedAll(haves);
        }

        internal static void UpgradeUnit(Player player, UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_UPGRADE_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_UPGRADE_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_UPGRADE_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_UPGRADE_PAWN);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_UPGRADE_PAWN);
                    break;

                case UnitTypes.PawnSword:
                    throw new Exception();

                case UnitTypes.Rook:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_UPGRADE_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_UPGRADE_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_UPGRADE_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_UPGRADE_ROOK);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_UPGRADE_ROOK);
                    break;

                case UnitTypes.RookCrossbow:
                    throw new Exception();

                case UnitTypes.Bishop:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, StartVGC.FOOD_FOR_UPGRADE_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, StartVGC.WOOD_FOR_UPGRADE_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, StartVGC.ORE_FOR_UPGRADE_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, StartVGC.IRON_FOR_UPGRADE_BISHOP);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, StartVGC.GOLD_FOR_UPGRADE_BISHOP);
                    break;

                case UnitTypes.BishopCrossbow:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static bool CanUpgradeBuildings(Player player, BuildingTypes buildingType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    haves[FOOD_NUMBER] = StartVGC.FoodForUpgradeFarm == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForUpgradeFarm;
                    haves[WOOD_NUMBER] = StartVGC.WoodForUpgradeFarm == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForUpgradeFarm;
                    haves[ORE_NUMBER] = StartVGC.OreForUpgradeFarm == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForUpgradeFarm;
                    haves[IRON_NUMBER] = StartVGC.IronForUpgradeFarm == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForUpgradeFarm;
                    haves[GOLD_NUMBER] = StartVGC.GoldForUpgradeFarm == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForUpgradeFarm;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[FOOD_NUMBER] = StartVGC.FoodForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForUpgradeWoodcutter;
                    haves[WOOD_NUMBER] = StartVGC.WoodForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForUpgradeWoodcutter;
                    haves[ORE_NUMBER] = StartVGC.OreForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForUpgradeWoodcutter;
                    haves[IRON_NUMBER] = StartVGC.IronForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForUpgradeWoodcutter;
                    haves[GOLD_NUMBER] = StartVGC.GoldForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForUpgradeWoodcutter;
                    break;

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = StartVGC.FoodForUpgradeMine == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= StartVGC.FoodForUpgradeMine;
                    haves[WOOD_NUMBER] = StartVGC.WoodForUpgradeMine == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= StartVGC.WoodForUpgradeMine;
                    haves[ORE_NUMBER] = StartVGC.OreForUpgradeMine == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= StartVGC.OreForUpgradeMine;
                    haves[IRON_NUMBER] = StartVGC.IronForUpgradeMine == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= StartVGC.IronForUpgradeMine;
                    haves[GOLD_NUMBER] = StartVGC.GoldForUpgradeMine == NULL_RESOURCES ? true : Instance.EGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= StartVGC.GoldForUpgradeMine;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
        }

        internal static void UpgradeBuildings(Player player, BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, 5);
                    break;

                case BuildingTypes.Farm:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, 5);
                    break;

                case BuildingTypes.Woodcutter:
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, 0);
                    Instance.EGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, 5);
                    break;

                case BuildingTypes.Mine:
                    break;

                default:
                    throw new Exception();
            }


            Instance.EGGM.BuildingsEnt_UpgradeBuildingsCom.AddAmountUpgrades(buildingType, player.IsMasterClient);
        }

        private static bool HavedAll(bool[] haves) => haves[FOOD_NUMBER] && haves[WOOD_NUMBER] && haves[ORE_NUMBER] && haves[IRON_NUMBER] && haves[GOLD_NUMBER];
    }
}