using Assets.Scripts.Abstractions;
using Photon.Realtime;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public sealed class EconomyManager
    {
        private EntitiesGameGeneralManager _eGM;

        internal EconomyManager(EntitiesGameGeneralManager eGM)
        {
            _eGM = eGM;
        }

        internal bool CanCreateBuilding(BuildingTypes buildingType, Player player, out bool[] haves)
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
                    haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                    haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                    haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                    haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                    haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                    break;

                case BuildingTypes.Woodcutter:
                    haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                    haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                    break;

                case BuildingTypes.Mine:
                    haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                    haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                    haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                    haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                    haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                    break;

                default:
                    break;
            }

            if (haves[ValuesConst.FOOD_NUMBER] && haves[ValuesConst.WOOD_NUMBER] && haves[ValuesConst.ORE_NUMBER] && haves[ValuesConst.IRON_NUMBER] && haves[ValuesConst.GOLD_NUMBER]) return true;
            else return false;
        }

        internal void CreateBuilding(BuildingTypes buildingType, Player player)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    break;

                case BuildingTypes.Farm:
                    _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM);
                    _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM);
                    _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUILDING_FARM);
                    _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUILDING_FARM);
                    _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM);
                    break;

                case BuildingTypes.Woodcutter:
                    _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER);
                    _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER);
                    _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER);
                    _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER);
                    _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER);
                    break;

                case BuildingTypes.Mine:
                    _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE);
                    _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE);
                    _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUILDING_MINE);
                    _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUILDING_MINE);
                    _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE);
                    break;

                default:
                    break;
            }
        }


        internal bool CanCreateUnit(UnitTypes unitType, Player player, out bool[] haves)
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
                    haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;
                    break;

                case UnitTypes.Rook:
                    haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;
                    break;

                case UnitTypes.Bishop:
                    haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;
                    break;

                default:
                    break;
            }

            if (haves[ValuesConst.FOOD_NUMBER] && haves[ValuesConst.WOOD_NUMBER] && haves[ValuesConst.ORE_NUMBER] && haves[ValuesConst.IRON_NUMBER] && haves[ValuesConst.GOLD_NUMBER]) return true;
            else return false;
        }

        internal void CreateUnit(UnitTypes unitType, Player player)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUYING_PAWN);
                    _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUYING_PAWN);
                    _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUYING_PAWN);
                    _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUYING_PAWN);
                    _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUYING_PAWN);
                    break;

                case UnitTypes.Rook:
                    _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUYING_ROOK);
                    _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUYING_ROOK);
                    _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUYING_ROOK);
                    _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUYING_ROOK);
                    _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUYING_ROOK);
                    break;

                case UnitTypes.Bishop:
                    _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_BUYING_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_BUYING_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP);
                    break;

                default:
                    break;
            }

            _eGM.UnitInventorEnt_UnitInventorCom.AddAmountUnits(unitType, player.IsMasterClient);
        }


        internal bool CanMeltOre(Player player, out bool[] haves)
        {
            haves = new bool[ValuesConst.AMOUNT_RESOURCES_TYPES];
            for (int i = 0; i < haves.Length; i++) haves[i] = true;

            haves[ValuesConst.FOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) >= Instance.StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haves[ValuesConst.WOOD_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) >= Instance.StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haves[ValuesConst.ORE_NUMBER] = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) >= Instance.StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haves[ValuesConst.IRON_NUMBER] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= Instance.StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haves[ValuesConst.GOLD_NUMBER] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= Instance.StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haves[ValuesConst.FOOD_NUMBER] && haves[ValuesConst.WOOD_NUMBER] && haves[ValuesConst.ORE_NUMBER] && haves[ValuesConst.IRON_NUMBER] && haves[ValuesConst.GOLD_NUMBER]) return true;
            else return false;
        }

        internal void MeltOre(Player player)
        {
            _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, player.IsMasterClient) - Instance.StartValuesGameConfig.FOOD_FOR_MELTING_ORE);
            _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, player.IsMasterClient) - Instance.StartValuesGameConfig.WOOD_FOR_MELTING_ORE);
            _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, player.IsMasterClient) - Instance.StartValuesGameConfig.ORE_FOR_MELTING_ORE);
            _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - Instance.StartValuesGameConfig.IRON_FOR_MELTING_ORE);
            _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - Instance.StartValuesGameConfig.GOLD_FOR_MELTING_ORE);

            _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) + 1);
            _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) + 1);
        }
    }
}