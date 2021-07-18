﻿using Assets.Scripts.Abstractions.ValuesConsts;
using Photon.Realtime;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.ValuesConst;
using static Assets.Scripts.Abstractions.ValuesConsts.EconomyValues;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public static class EconomyManager
    {
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
                    haves[FOOD_NUMBER] = FOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUILDING_FARM;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUILDING_FARM;
                    haves[ORE_NUMBER] = ORE_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUILDING_FARM;
                    haves[IRON_NUMBER] = IRON_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUILDING_FARM;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUILDING_FARM;
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUILDING_MINE;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUILDING_MINE;
                    haves[ORE_NUMBER] = ORE_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUILDING_MINE;
                    haves[IRON_NUMBER] = IRON_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUILDING_MINE;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUILDING_MINE;
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
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUILDING_FARM);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUILDING_FARM);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUILDING_FARM);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUILDING_FARM);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUILDING_FARM);
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUILDING_MINE);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUILDING_MINE);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUILDING_MINE);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUILDING_MINE);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUILDING_MINE);
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
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUYING_PAWN;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUYING_PAWN;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUYING_PAWN;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUYING_PAWN;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUYING_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUYING_ROOK;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUYING_ROOK;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUYING_ROOK;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUYING_ROOK;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUYING_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUYING_BISHOP;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUYING_BISHOP;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUYING_BISHOP;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUYING_BISHOP;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUYING_BISHOP;
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
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUYING_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUYING_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUYING_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUYING_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUYING_PAWN);
                    break;

                case UnitTypes.Rook:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUYING_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUYING_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUYING_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUYING_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUYING_ROOK);
                    break;

                case UnitTypes.Bishop:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUYING_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUYING_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUYING_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUYING_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUYING_BISHOP);
                    break;

                default:
                    break;
            }
        }


        internal static bool CanMeltOre(Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            haves[FOOD_NUMBER] = FOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_MELTING_ORE;
            haves[WOOD_NUMBER] = WOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_MELTING_ORE;
            haves[ORE_NUMBER] = ORE_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_MELTING_ORE;
            haves[IRON_NUMBER] = IRON_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_MELTING_ORE;
            haves[GOLD_NUMBER] = GOLD_FOR_MELTING_ORE == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_MELTING_ORE;


            return HavedAll(haves);
        }

        internal static void MeltOre(Player player)
        {
            Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_MELTING_ORE);
            Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_MELTING_ORE);
            Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_MELTING_ORE);
            Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_MELTING_ORE);
            Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_MELTING_ORE);

            Instance.EntGGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Iron, player.IsMasterClient, 3);
            Instance.EntGGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Gold, player.IsMasterClient, 1);
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
                    haves[FOOD_NUMBER] = FoodForPawnFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForPawnFire;
                    haves[WOOD_NUMBER] = WoodForPawnFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForPawnFire;
                    haves[ORE_NUMBER] = OreForPawnFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForPawnFire;
                    haves[IRON_NUMBER] = IronForPawnFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForPawnFire;
                    haves[GOLD_NUMBER] = GoldForPawnFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForPawnFire;
                    break;

                case UnitTypes.PawnSword:
                    haves[FOOD_NUMBER] = FoodForPawnSwordFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForPawnSwordFire;
                    haves[WOOD_NUMBER] = WoodForPawnSwordFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForPawnSwordFire;
                    haves[ORE_NUMBER] = OreForPawnSwordFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForPawnSwordFire;
                    haves[IRON_NUMBER] = IronForPawnSwordFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForPawnSwordFire;
                    haves[GOLD_NUMBER] = GoldForPawnSwordFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForPawnSwordFire;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FoodForRookFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForRookFire;
                    haves[WOOD_NUMBER] = WoodForRookFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForRookFire;
                    haves[ORE_NUMBER] = OreForRookFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForRookFire;
                    haves[IRON_NUMBER] = IronForRookFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForRookFire;
                    haves[GOLD_NUMBER] = GoldForRookFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForRookFire;
                    break;

                case UnitTypes.RookCrossbow:
                    haves[FOOD_NUMBER] = FoodForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForRookCrossbowFire;
                    haves[WOOD_NUMBER] = WoodForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForRookCrossbowFire;
                    haves[ORE_NUMBER] = OreForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForRookCrossbowFire;
                    haves[IRON_NUMBER] = IronForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForRookCrossbowFire;
                    haves[GOLD_NUMBER] = GoldForRookCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForRookCrossbowFire;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FoodForBishopFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForBishopFire;
                    haves[WOOD_NUMBER] = WoodForBishopFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForBishopFire;
                    haves[ORE_NUMBER] = OreForBishopFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForBishopFire;
                    haves[IRON_NUMBER] = IronForBishopFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForBishopFire;
                    haves[GOLD_NUMBER] = GoldForBishopFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForBishopFire;
                    break;

                case UnitTypes.BishopCrossbow:
                    haves[FOOD_NUMBER] = FoodForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForBishopCrossbowFire;
                    haves[WOOD_NUMBER] = WoodForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForBishopCrossbowFire;
                    haves[ORE_NUMBER] = OreForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForBishopCrossbowFire;
                    haves[IRON_NUMBER] = IronForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForBishopCrossbowFire;
                    haves[GOLD_NUMBER] = GoldForBishopCrossbowFire == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForBishopCrossbowFire;
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
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForPawnFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForPawnFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForPawnFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForPawnFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForPawnFire);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForPawnSwordFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForPawnSwordFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForPawnSwordFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForPawnSwordFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForPawnSwordFire);
                    break;

                case UnitTypes.Rook:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForRookFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForRookFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForRookFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForRookFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForRookFire);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForRookCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForRookCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForRookCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForRookCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForRookCrossbowFire);
                    break;

                case UnitTypes.Bishop:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForBishopFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForBishopFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForBishopFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForBishopFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForBishopFire);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForBishopCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForBishopCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForBishopCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForBishopCrossbowFire);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForBishopCrossbowFire);
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
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_PAWN;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_UPGRADE_PAWN;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_UPGRADE_PAWN;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_UPGRADE_PAWN;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_UPGRADE_PAWN;
                    break;

                case UnitTypes.PawnSword:
                    throw new Exception();

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_ROOK;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_UPGRADE_ROOK;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_UPGRADE_ROOK;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_UPGRADE_ROOK;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_UPGRADE_ROOK;
                    break;

                case UnitTypes.RookCrossbow:
                    throw new Exception();

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_BISHOP;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_UPGRADE_BISHOP;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_UPGRADE_BISHOP;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_UPGRADE_BISHOP;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_UPGRADE_BISHOP;
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
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_UPGRADE_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_UPGRADE_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_UPGRADE_PAWN);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_UPGRADE_PAWN);
                    break;

                case UnitTypes.PawnSword:
                    throw new Exception();

                case UnitTypes.Rook:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_UPGRADE_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_UPGRADE_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_UPGRADE_ROOK);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_UPGRADE_ROOK);
                    break;

                case UnitTypes.RookCrossbow:
                    throw new Exception();

                case UnitTypes.Bishop:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_UPGRADE_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_UPGRADE_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_UPGRADE_BISHOP);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_UPGRADE_BISHOP);
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
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_FARM == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_FARM;
                    haves[WOOD_NUMBER] = WoodForUpgradeFarm == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForUpgradeFarm;
                    haves[ORE_NUMBER] = OreForUpgradeFarm == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForUpgradeFarm;
                    haves[IRON_NUMBER] = IronForUpgradeFarm == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForUpgradeFarm;
                    haves[GOLD_NUMBER] = GoldForUpgradeFarm == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForUpgradeFarm;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[FOOD_NUMBER] = FoodForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForUpgradeWoodcutter;
                    haves[WOOD_NUMBER] = WoodForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForUpgradeWoodcutter;
                    haves[ORE_NUMBER] = OreForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForUpgradeWoodcutter;
                    haves[IRON_NUMBER] = IronForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForUpgradeWoodcutter;
                    haves[GOLD_NUMBER] = GoldForUpgradeWoodcutter == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForUpgradeWoodcutter;
                    break;

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = FoodForUpgradeMine == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForUpgradeMine;
                    haves[WOOD_NUMBER] = WoodForUpgradeMine == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForUpgradeMine;
                    haves[ORE_NUMBER] = OreForUpgradeMine == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForUpgradeMine;
                    haves[IRON_NUMBER] = IronForUpgradeMine == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForUpgradeMine;
                    haves[GOLD_NUMBER] = GoldForUpgradeMine == NULL_RESOURCES ? true : Instance.EntGGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForUpgradeMine;
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
                    break;

                case BuildingTypes.Farm:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_FARM);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForUpgradeFarm);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForUpgradeFarm);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForUpgradeFarm);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForUpgradeFarm);
                    break;

                case BuildingTypes.Woodcutter:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForUpgradeWoodcutter);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForUpgradeWoodcutter);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForUpgradeWoodcutter);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForUpgradeWoodcutter);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForUpgradeWoodcutter);
                    break;

                case BuildingTypes.Mine:
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForUpgradeMine);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForUpgradeMine);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForUpgradeMine);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForUpgradeMine);
                    Instance.EntGGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForUpgradeMine);
                    break;

                default:
                    throw new Exception();
            }


            Instance.EntGGM.BuildingsEnt_UpgradeBuildingsCom.AddAmountUpgrades(buildingType, player.IsMasterClient);
        }

        private static bool HavedAll(bool[] haves) => haves[FOOD_NUMBER] && haves[WOOD_NUMBER] && haves[ORE_NUMBER] && haves[IRON_NUMBER] && haves[GOLD_NUMBER];
    }
}