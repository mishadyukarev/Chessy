using System;
using System.Collections.Generic;
using static Scripts.Game.EconomyValues;

namespace Scripts.Game
{
    internal struct InventResourCom
    {
        private Dictionary<PlayerTypes, Dictionary<ResourceTypes, int>> _amountResources;

        internal InventResourCom(bool needNew) : this()
        {
            if (needNew)
            {
                _amountResources = new Dictionary<PlayerTypes, Dictionary<ResourceTypes, int>>();

                _amountResources[PlayerTypes.First] = new Dictionary<ResourceTypes, int>();
                _amountResources[PlayerTypes.Second] = new Dictionary<ResourceTypes, int>();

                for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                {
                    _amountResources[PlayerTypes.First].Add(resourceType, default);
                    _amountResources[PlayerTypes.Second].Add(resourceType, default);
                }
            }
        }

        internal int AmountResources(PlayerTypes playerType, ResourceTypes resourceTypes) => _amountResources[playerType][resourceTypes];
        internal void SetAmountResources(PlayerTypes playerType, ResourceTypes resourceType, int value) => _amountResources[playerType][resourceType] = value;

        internal void AddAmountResources(PlayerTypes playerType, ResourceTypes resourceType, int adding = 1) => SetAmountResources(playerType, resourceType, AmountResources(playerType, resourceType) + adding);
        internal void TakeAmountResources(PlayerTypes playerType, ResourceTypes resourceType, int taking = 1) => SetAmountResources(playerType, resourceType, AmountResources(playerType, resourceType) - taking);






        internal bool CanCreateBuild(PlayerTypes playerType, BuildingTypes buildingType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return true;

                case BuildingTypes.Farm:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_BUILDING_FARM;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_BUILDING_FARM;
                    haves[ORE_NUMBER] = ORE_FOR_BUILDING_FARM == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_BUILDING_FARM;
                    haves[IRON_NUMBER] = IRON_FOR_BUILDING_FARM == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_BUILDING_FARM;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_BUILDING_FARM;
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_BUILDING_MINE;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_BUILDING_MINE;
                    haves[ORE_NUMBER] = ORE_FOR_BUILDING_MINE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_BUILDING_MINE;
                    haves[IRON_NUMBER] = IRON_FOR_BUILDING_MINE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_BUILDING_MINE;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_BUILDING_MINE;
                    break;

                default:
                    throw new Exception();
            }

            return HavedAll(haves);
        }
        internal void BuyBuild(PlayerTypes playerType, BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_BUILDING_FARM);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_BUILDING_FARM);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_BUILDING_FARM);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_BUILDING_FARM);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_BUILDING_FARM);
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_BUILDING_MINE);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_BUILDING_MINE);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_BUILDING_MINE);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_BUILDING_MINE);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_BUILDING_MINE);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_BUYING_PAWN;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_BUYING_PAWN;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_BUYING_PAWN;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_BUYING_PAWN;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_BUYING_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_BUYING_ROOK;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_BUYING_ROOK;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_BUYING_ROOK;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_BUYING_ROOK;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_BUYING_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_BUYING_BISHOP;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_BUYING_BISHOP;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_BUYING_BISHOP;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_BUYING_BISHOP;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_BUYING_BISHOP;
                    break;

                default:
                    throw new Exception();
            }

            return HavedAll(haves);
        }
        internal void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_BUYING_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_BUYING_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_BUYING_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_BUYING_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_BUYING_PAWN);
                    break;

                case UnitTypes.Rook:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_BUYING_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_BUYING_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_BUYING_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_BUYING_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_BUYING_ROOK);
                    break;

                case UnitTypes.Bishop:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_BUYING_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_BUYING_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_BUYING_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_BUYING_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_BUYING_BISHOP);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal bool CanMeltOre(PlayerTypes playerType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            haves[FOOD_NUMBER] = FOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_MELTING_ORE;
            haves[WOOD_NUMBER] = WOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_MELTING_ORE;
            haves[ORE_NUMBER] = ORE_FOR_MELTING_ORE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_MELTING_ORE;
            haves[IRON_NUMBER] = IRON_FOR_MELTING_ORE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_MELTING_ORE;
            haves[GOLD_NUMBER] = GOLD_FOR_MELTING_ORE == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_MELTING_ORE;


            return HavedAll(haves);
        }
        internal void BuyMeltOre(PlayerTypes playerType)
        {
            TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_MELTING_ORE);
            TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_MELTING_ORE);
            TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_MELTING_ORE);
            TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_MELTING_ORE);
            TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_MELTING_ORE);

            AddAmountResources(playerType, ResourceTypes.Iron, 4);
            AddAmountResources(playerType, ResourceTypes.Gold, 1);
        }


        internal bool CanUpgradeUnit(PlayerTypes playerType, UnitTypes unitType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_UPGRADE_PAWN;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_UPGRADE_PAWN;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_UPGRADE_PAWN;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_UPGRADE_PAWN;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_UPGRADE_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_UPGRADE_ROOK;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_UPGRADE_ROOK;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_UPGRADE_ROOK;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_UPGRADE_ROOK;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_UPGRADE_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_UPGRADE_BISHOP;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WOOD_FOR_UPGRADE_BISHOP;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= ORE_FOR_UPGRADE_BISHOP;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IRON_FOR_UPGRADE_BISHOP;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GOLD_FOR_UPGRADE_BISHOP;
                    break;

                default:
                    throw new Exception();
            }

            return HavedAll(haves);
        }
        internal void BuyUpgradeUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_UPGRADE_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_UPGRADE_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_UPGRADE_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_UPGRADE_PAWN);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_UPGRADE_PAWN);
                    break;

                case UnitTypes.Rook:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_UPGRADE_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_UPGRADE_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_UPGRADE_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_UPGRADE_ROOK);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_UPGRADE_ROOK);
                    break;

                case UnitTypes.Bishop:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WOOD_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Ore, ORE_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IRON_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GOLD_FOR_UPGRADE_BISHOP);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal bool CanUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_FARM == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FOOD_FOR_UPGRADE_FARM;
                    haves[WOOD_NUMBER] = WoodForUpgradeFarm == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WoodForUpgradeFarm;
                    haves[ORE_NUMBER] = OreForUpgradeFarm == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= OreForUpgradeFarm;
                    haves[IRON_NUMBER] = IronForUpgradeFarm == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IronForUpgradeFarm;
                    haves[GOLD_NUMBER] = GoldForUpgradeFarm == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GoldForUpgradeFarm;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[FOOD_NUMBER] = FoodForUpgradeWoodcutter == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FoodForUpgradeWoodcutter;
                    haves[WOOD_NUMBER] = WoodForUpgradeWoodcutter == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WoodForUpgradeWoodcutter;
                    haves[ORE_NUMBER] = OreForUpgradeWoodcutter == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= OreForUpgradeWoodcutter;
                    haves[IRON_NUMBER] = IronForUpgradeWoodcutter == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IronForUpgradeWoodcutter;
                    haves[GOLD_NUMBER] = GoldForUpgradeWoodcutter == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GoldForUpgradeWoodcutter;
                    break;

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = FoodForUpgradeMine == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= FoodForUpgradeMine;
                    haves[WOOD_NUMBER] = WoodForUpgradeMine == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= WoodForUpgradeMine;
                    haves[ORE_NUMBER] = OreForUpgradeMine == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= OreForUpgradeMine;
                    haves[IRON_NUMBER] = IronForUpgradeMine == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= IronForUpgradeMine;
                    haves[GOLD_NUMBER] = GoldForUpgradeMine == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= GoldForUpgradeMine;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
        }
        internal void BuyUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    break;

                case BuildingTypes.Farm:
                    TakeAmountResources(playerType, ResourceTypes.Food, FOOD_FOR_UPGRADE_FARM);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WoodForUpgradeFarm);
                    TakeAmountResources(playerType, ResourceTypes.Ore, OreForUpgradeFarm);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IronForUpgradeFarm);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GoldForUpgradeFarm);
                    break;

                case BuildingTypes.Woodcutter:
                    TakeAmountResources(playerType, ResourceTypes.Food, FoodForUpgradeWoodcutter);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WoodForUpgradeWoodcutter);
                    TakeAmountResources(playerType, ResourceTypes.Ore, OreForUpgradeWoodcutter);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IronForUpgradeWoodcutter);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GoldForUpgradeWoodcutter);
                    break;

                case BuildingTypes.Mine:
                    TakeAmountResources(playerType, ResourceTypes.Food, FoodForUpgradeMine);
                    TakeAmountResources(playerType, ResourceTypes.Wood, WoodForUpgradeMine);
                    TakeAmountResources(playerType, ResourceTypes.Ore, OreForUpgradeMine);
                    TakeAmountResources(playerType, ResourceTypes.Iron, IronForUpgradeMine);
                    TakeAmountResources(playerType, ResourceTypes.Gold, GoldForUpgradeMine);
                    break;

                default:
                    throw new Exception();
            }
        }

        private bool HavedAll(bool[] haves) => haves[FOOD_NUMBER] && haves[WOOD_NUMBER] && haves[ORE_NUMBER] && haves[IRON_NUMBER] && haves[GOLD_NUMBER];
    }
}

