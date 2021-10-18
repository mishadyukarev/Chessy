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
        internal void SetAmountResAll(ResourceTypes resourceType, int value)
        {
            for (PlayerTypes playerType = (PlayerTypes)1; playerType < (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length; playerType++)
            {
                _amountResources[playerType][resourceType] = value;
            }
        }

        internal void AddAmountResources(PlayerTypes playerType, ResourceTypes resourceType, int adding = 1) => SetAmountResources(playerType, resourceType, AmountResources(playerType, resourceType) + adding);
        internal void TakeAmountResources(PlayerTypes playerType, ResourceTypes resourceType, int taking = 1) => SetAmountResources(playerType, resourceType, AmountResources(playerType, resourceType) - taking);






        internal bool CanCreateBuild(PlayerTypes playerType, BuildingTypes buildingType, out bool[] haves)
        {
            haves = new bool[AMOUNT_RESOURCES_TYPES];


            haves[FOOD_NUMBER] = AmountResForBuild(buildingType, ResourceTypes.Food) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= AmountResForBuild(buildingType, ResourceTypes.Food);
            haves[WOOD_NUMBER] = AmountResForBuild(buildingType, ResourceTypes.Wood) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= AmountResForBuild(buildingType, ResourceTypes.Wood);
            haves[ORE_NUMBER] = AmountResForBuild(buildingType, ResourceTypes.Ore) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= AmountResForBuild(buildingType, ResourceTypes.Ore);
            haves[IRON_NUMBER] = AmountResForBuild(buildingType, ResourceTypes.Iron) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= AmountResForBuild(buildingType, ResourceTypes.Iron);
            haves[GOLD_NUMBER] = AmountResForBuild(buildingType, ResourceTypes.Gold) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= AmountResForBuild(buildingType, ResourceTypes.Gold);



            return HavedAll(haves);
        }
        internal void BuyBuild(PlayerTypes playerType, BuildingTypes buildingType)
        {
            TakeAmountResources(playerType, ResourceTypes.Food, AmountResForBuild(buildingType, ResourceTypes.Food));
            TakeAmountResources(playerType, ResourceTypes.Wood, AmountResForBuild(buildingType, ResourceTypes.Wood));
            TakeAmountResources(playerType, ResourceTypes.Ore, AmountResForBuild(buildingType, ResourceTypes.Ore));
            TakeAmountResources(playerType, ResourceTypes.Iron, AmountResForBuild(buildingType, ResourceTypes.Iron));
            TakeAmountResources(playerType, ResourceTypes.Gold, AmountResForBuild(buildingType, ResourceTypes.Gold));
        }

        internal bool CanCreateUnit(PlayerTypes playerType, UnitTypes unitType, out bool[] haves)
        {
            haves = new bool[Enum.GetNames(typeof(ResourceTypes)).Length];

            haves[FOOD_NUMBER] = AmountResForBuy(unitType, ResourceTypes.Food) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Food) >= AmountResForBuy(unitType, ResourceTypes.Food);
            haves[WOOD_NUMBER] = AmountResForBuy(unitType, ResourceTypes.Wood) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Wood) >= AmountResForBuy(unitType, ResourceTypes.Wood);
            haves[ORE_NUMBER] = AmountResForBuy(unitType, ResourceTypes.Ore) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Ore) >= AmountResForBuy(unitType, ResourceTypes.Ore);
            haves[IRON_NUMBER] = AmountResForBuy(unitType, ResourceTypes.Iron) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Iron) >= AmountResForBuy(unitType, ResourceTypes.Iron);
            haves[GOLD_NUMBER] = AmountResForBuy(unitType, ResourceTypes.Gold) == NULL_RESOURCES ? true : AmountResources(playerType, ResourceTypes.Gold) >= AmountResForBuy(unitType, ResourceTypes.Gold);

            return HavedAll(haves);
        }
        internal void BuyCreateUnit(PlayerTypes playerType, UnitTypes unitType)
        {
            for (ResourceTypes resourceTypes = (ResourceTypes)1; resourceTypes < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceTypes++)
                TakeAmountResources(playerType, resourceTypes, AmountResForBuy(unitType, resourceTypes));
        }

        internal bool CanMeltOre(PlayerTypes playerType, out bool[] haves)
        {
            haves = new bool[Enum.GetNames(typeof(ResourceTypes)).Length];

            int i = 0;
            for (var resType = (ResourceTypes)1; resType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resType++)
            {
                haves[i] = AmountResForMelting(resType) == NULL_RESOURCES ? true : AmountResources(playerType, resType) >= AmountResForMelting(resType);
                i++;
            }
                

            return HavedAll(haves);
        }
        internal void BuyMeltOre(PlayerTypes playerType)
        {
            for (var resourceTypes = (ResourceTypes)1; resourceTypes < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceTypes++)
                TakeAmountResources(playerType, resourceTypes, AmountResForMelting(resourceTypes));

            AddAmountResources(playerType, ResourceTypes.Iron, 4);
            AddAmountResources(playerType, ResourceTypes.Gold, 1);
        }

        internal bool CanUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType, out bool[] haves)
        {
            haves = new bool[Enum.GetNames(typeof(ResourceTypes)).Length];

            int i = 0;
            for (var resType = (ResourceTypes)1; resType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resType++)
            {
                haves[i] = AmountResForUpgrade(buildingType, resType) == NULL_RESOURCES ? true : AmountResources(playerType, resType) >= AmountResForUpgrade(buildingType, resType);
                i++;
            }
                

            return HavedAll(haves);
        }
        internal void BuyUpgradeBuildings(PlayerTypes playerType, BuildingTypes buildingType)
        {
            for (var resourceTypes = (ResourceTypes)1; resourceTypes < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceTypes++)
                TakeAmountResources(playerType, resourceTypes, AmountResForUpgrade(buildingType, resourceTypes));
        }

        private bool HavedAll(bool[] haves) => haves[FOOD_NUMBER] && haves[WOOD_NUMBER] && haves[ORE_NUMBER] && haves[IRON_NUMBER] && haves[GOLD_NUMBER];
    }
}

