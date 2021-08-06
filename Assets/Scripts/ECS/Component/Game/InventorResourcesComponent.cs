using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.EconomyValues;

namespace Assets.Scripts.ECS.Component.Game
{
    internal struct InventorResourcesComponent
    {
        private Dictionary<ResourceTypes, Dictionary<bool, int>> _amountResources;

        internal InventorResourcesComponent(Dictionary<ResourceTypes, Dictionary<bool, int>> amountResources)
        {
            _amountResources = amountResources;

            for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
            {
                var dic = new Dictionary<bool, int>();
                dic.Add(true, default);
                dic.Add(false, default);

                _amountResources.Add(resourceType, dic);
            }
        }

        internal int GetAmountResources(ResourceTypes resourceTypes, bool key) => _amountResources[resourceTypes][key];
        internal void SetAmountResources(ResourceTypes resourceType, bool key, int value) => _amountResources[resourceType][key] = value;

        internal void AddAmountResources(ResourceTypes resourceType, bool key, int adding = 1) => SetAmountResources(resourceType, key, GetAmountResources(resourceType, key) + adding);
        internal void TakeAmountResources(ResourceTypes resourceType, bool key, int taking = 1) => SetAmountResources(resourceType, key, GetAmountResources(resourceType, key) - taking);






        internal bool CanCreateNewBuilding(BuildingTypes buildingType, Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    return true;

                case BuildingTypes.Farm:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUILDING_FARM;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUILDING_FARM;
                    haves[ORE_NUMBER] = ORE_FOR_BUILDING_FARM == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUILDING_FARM;
                    haves[IRON_NUMBER] = IRON_FOR_BUILDING_FARM == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUILDING_FARM;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUILDING_FARM == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUILDING_FARM;
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUILDING_MINE;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUILDING_MINE;
                    haves[ORE_NUMBER] = ORE_FOR_BUILDING_MINE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUILDING_MINE;
                    haves[IRON_NUMBER] = IRON_FOR_BUILDING_MINE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUILDING_MINE;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUILDING_MINE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUILDING_MINE;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
        }
        internal void BuyNewBuilding(BuildingTypes buildingType, Player player)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUILDING_FARM);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUILDING_FARM);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUILDING_FARM);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUILDING_FARM);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUILDING_FARM);
                    break;

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUILDING_MINE);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUILDING_MINE);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUILDING_MINE);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUILDING_MINE);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUILDING_MINE);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal bool CanCreateUnit(UnitTypes unitType, Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUYING_PAWN;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUYING_PAWN;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUYING_PAWN;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUYING_PAWN;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUYING_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUYING_ROOK;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUYING_ROOK;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUYING_ROOK;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUYING_ROOK;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUYING_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_BUYING_BISHOP;
                    haves[WOOD_NUMBER] = WOOD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_BUYING_BISHOP;
                    haves[ORE_NUMBER] = ORE_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_BUYING_BISHOP;
                    haves[IRON_NUMBER] = IRON_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_BUYING_BISHOP;
                    haves[GOLD_NUMBER] = GOLD_FOR_BUYING_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_BUYING_BISHOP;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
        }
        internal void BuyCreateUnit(UnitTypes unitType, Player player)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUYING_PAWN);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUYING_PAWN);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUYING_PAWN);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUYING_PAWN);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUYING_PAWN);
                    break;

                case UnitTypes.Rook:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUYING_ROOK);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUYING_ROOK);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUYING_ROOK);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUYING_ROOK);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUYING_ROOK);
                    break;

                case UnitTypes.Bishop:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_BUYING_BISHOP);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_BUYING_BISHOP);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_BUYING_BISHOP);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_BUYING_BISHOP);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_BUYING_BISHOP);
                    break;

                default:
                    break;
            }
        }

        internal bool CanMeltOre(Player player, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            haves[FOOD_NUMBER] = FOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_MELTING_ORE;
            haves[WOOD_NUMBER] = WOOD_FOR_MELTING_ORE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_MELTING_ORE;
            haves[ORE_NUMBER] = ORE_FOR_MELTING_ORE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_MELTING_ORE;
            haves[IRON_NUMBER] = IRON_FOR_MELTING_ORE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_MELTING_ORE;
            haves[GOLD_NUMBER] = GOLD_FOR_MELTING_ORE == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_MELTING_ORE;


            return HavedAll(haves);
        }
        internal void BuyMeltOre(Player player)
        {
            TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_MELTING_ORE);
            TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_MELTING_ORE);
            TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_MELTING_ORE);
            TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_MELTING_ORE);
            TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_MELTING_ORE);

            AddAmountResources(ResourceTypes.Iron, player.IsMasterClient, 3);
            AddAmountResources(ResourceTypes.Gold, player.IsMasterClient, 1);
        }


        internal bool CanUpgradeUnit(Player player, UnitTypes unitType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_PAWN;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_UPGRADE_PAWN;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_UPGRADE_PAWN;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_UPGRADE_PAWN;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_PAWN == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_UPGRADE_PAWN;
                    break;

                case UnitTypes.PawnSword:
                    throw new Exception();

                case UnitTypes.Rook:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_ROOK;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_UPGRADE_ROOK;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_UPGRADE_ROOK;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_UPGRADE_ROOK;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_ROOK == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_UPGRADE_ROOK;
                    break;

                case UnitTypes.RookCrossbow:
                    throw new Exception();

                case UnitTypes.Bishop:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_BISHOP;
                    haves[WOOD_NUMBER] = WOOD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WOOD_FOR_UPGRADE_BISHOP;
                    haves[ORE_NUMBER] = ORE_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= ORE_FOR_UPGRADE_BISHOP;
                    haves[IRON_NUMBER] = IRON_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IRON_FOR_UPGRADE_BISHOP;
                    haves[GOLD_NUMBER] = GOLD_FOR_UPGRADE_BISHOP == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GOLD_FOR_UPGRADE_BISHOP;
                    break;

                case UnitTypes.BishopCrossbow:
                    throw new Exception();

                default:
                    throw new Exception();
            }

            return HavedAll(haves);
        }
        internal void BuyUpgradeUnit(Player player, UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_PAWN);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_UPGRADE_PAWN);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_UPGRADE_PAWN);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_UPGRADE_PAWN);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_UPGRADE_PAWN);
                    break;

                case UnitTypes.PawnSword:
                    throw new Exception();

                case UnitTypes.Rook:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_ROOK);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_UPGRADE_ROOK);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_UPGRADE_ROOK);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_UPGRADE_ROOK);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_UPGRADE_ROOK);
                    break;

                case UnitTypes.RookCrossbow:
                    throw new Exception();

                case UnitTypes.Bishop:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WOOD_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, ORE_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IRON_FOR_UPGRADE_BISHOP);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GOLD_FOR_UPGRADE_BISHOP);
                    break;

                case UnitTypes.BishopCrossbow:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal bool CanUpgradeBuildings(Player player, BuildingTypes buildingType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];

            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    haves[FOOD_NUMBER] = FOOD_FOR_UPGRADE_FARM == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FOOD_FOR_UPGRADE_FARM;
                    haves[WOOD_NUMBER] = WoodForUpgradeFarm == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForUpgradeFarm;
                    haves[ORE_NUMBER] = OreForUpgradeFarm == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForUpgradeFarm;
                    haves[IRON_NUMBER] = IronForUpgradeFarm == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForUpgradeFarm;
                    haves[GOLD_NUMBER] = GoldForUpgradeFarm == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForUpgradeFarm;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[FOOD_NUMBER] = FoodForUpgradeWoodcutter == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForUpgradeWoodcutter;
                    haves[WOOD_NUMBER] = WoodForUpgradeWoodcutter == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForUpgradeWoodcutter;
                    haves[ORE_NUMBER] = OreForUpgradeWoodcutter == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForUpgradeWoodcutter;
                    haves[IRON_NUMBER] = IronForUpgradeWoodcutter == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForUpgradeWoodcutter;
                    haves[GOLD_NUMBER] = GoldForUpgradeWoodcutter == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForUpgradeWoodcutter;
                    break;

                case BuildingTypes.Mine:
                    haves[FOOD_NUMBER] = FoodForUpgradeMine == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Food, player.IsMasterClient) >= FoodForUpgradeMine;
                    haves[WOOD_NUMBER] = WoodForUpgradeMine == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Wood, player.IsMasterClient) >= WoodForUpgradeMine;
                    haves[ORE_NUMBER] = OreForUpgradeMine == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Ore, player.IsMasterClient) >= OreForUpgradeMine;
                    haves[IRON_NUMBER] = IronForUpgradeMine == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Iron, player.IsMasterClient) >= IronForUpgradeMine;
                    haves[GOLD_NUMBER] = GoldForUpgradeMine == NULL_RESOURCES ? true : GetAmountResources(ResourceTypes.Gold, player.IsMasterClient) >= GoldForUpgradeMine;
                    break;

                default:
                    break;
            }

            return HavedAll(haves);
        }
        internal void BuyUpgradeBuildings(Player player, BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    break;

                case BuildingTypes.Farm:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FOOD_FOR_UPGRADE_FARM);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForUpgradeFarm);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForUpgradeFarm);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForUpgradeFarm);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForUpgradeFarm);
                    break;

                case BuildingTypes.Woodcutter:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForUpgradeWoodcutter);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForUpgradeWoodcutter);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForUpgradeWoodcutter);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForUpgradeWoodcutter);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForUpgradeWoodcutter);
                    break;

                case BuildingTypes.Mine:
                    TakeAmountResources(ResourceTypes.Food, player.IsMasterClient, FoodForUpgradeMine);
                    TakeAmountResources(ResourceTypes.Wood, player.IsMasterClient, WoodForUpgradeMine);
                    TakeAmountResources(ResourceTypes.Ore, player.IsMasterClient, OreForUpgradeMine);
                    TakeAmountResources(ResourceTypes.Iron, player.IsMasterClient, IronForUpgradeMine);
                    TakeAmountResources(ResourceTypes.Gold, player.IsMasterClient, GoldForUpgradeMine);
                    break;

                default:
                    throw new Exception();
            }


            MainGameSystem.UpgradesBuildingsCom.AddAmountUpgrades(buildingType, player.IsMasterClient);
        }

        private bool HavedAll(bool[] haves) => haves[FOOD_NUMBER] && haves[WOOD_NUMBER] && haves[ORE_NUMBER] && haves[IRON_NUMBER] && haves[GOLD_NUMBER];
    }
}

