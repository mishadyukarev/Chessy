using Photon.Realtime;
using System;
using static Main;

internal sealed class EconomyManager
{
    private EntitiesGeneralManager _eGM;

    private int _amountResourse = 5;

    private int _numberFood = 0;
    private int _numberWood = 1;
    private int _numberOre = 2;
    private int _numberIron = 3;
    private int _numberGold = 4;

    private StartValuesGameConfig StartValuesGameConfig => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.StartValuesGameConfig;
    internal int AmountResourse => _amountResourse;
    internal int NumberFood => _numberFood;
    internal int NumberWood => _numberWood;
    internal int NumberOre => _numberOre;
    internal int NumberIron => _numberIron;
    internal int NumberGold => _numberGold;

    internal EconomyManager(ECSManager eCSmanager)
    {
        _eGM = eCSmanager.EntitiesGeneralManager;
    }

    internal bool CanCreateBuilding(BuildingTypes buildingType, Player player, out bool[] haves)
    {
        haves = new bool[_amountResourse];
        for (int i = 0; i < haves.Length; i++) haves[i] = true;

        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                return true;

            case BuildingTypes.Farm:
                haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                break;

            case BuildingTypes.Woodcutter:
                haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                break;

            case BuildingTypes.Mine:
                haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                break;

            default:
                break;
        }

        if (haves[_numberFood] && haves[_numberWood] && haves[_numberOre] && haves[_numberIron] && haves[_numberGold]) return true;
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
                _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_BUILDING_FARM);
                _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_BUILDING_FARM);
                _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_BUILDING_FARM);
                _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_BUILDING_FARM);
                _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_BUILDING_FARM);
                break;

            case BuildingTypes.Woodcutter:
                _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER);
                _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER);
                _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER);
                _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER);
                _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER);
                break;

            case BuildingTypes.Mine:
                _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_BUILDING_MINE);
                _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_BUILDING_MINE);
                _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_BUILDING_MINE);
                _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_BUILDING_MINE);
                _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_BUILDING_MINE);
                break;

            default:
                break;
        }
    }


    internal bool CanCreateUnit(UnitTypes unitType, Player player, out bool[] haves)
    {
        haves = new bool[_amountResourse];
        for (int i = 0; i < haves.Length; i++) haves[i] = true;

        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                throw new Exception();

            case UnitTypes.Pawn:
                haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;
                break;

            case UnitTypes.Rook:
                haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;
                break;

            case UnitTypes.Bishop:
                haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;
                break;

            default:
                break;
        }

        if (haves[_numberFood] && haves[_numberWood] && haves[_numberOre] && haves[_numberIron] && haves[_numberGold]) return true;
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
                _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_BUYING_PAWN);
                _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_BUYING_PAWN);
                _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_BUYING_PAWN);
                _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_BUYING_PAWN);
                _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_BUYING_PAWN);
                break;

            case UnitTypes.Rook:
                _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_BUYING_ROOK);
                _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_BUYING_ROOK);
                _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_BUYING_ROOK);
                _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_BUYING_ROOK);
                _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_BUYING_ROOK);
                break;

            case UnitTypes.Bishop:
                _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP);
                _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP);
                _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_BUYING_BISHOP);
                _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_BUYING_BISHOP);
                _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP);
                break;

            default:
                break;
        }

        _eGM.UnitInventorEnt_UnitInventorCom.AddAmountUnits(unitType, player.IsMasterClient);
    }


    internal bool CanMeltOre(Player player, out bool[] haves)
    {
        haves = new bool[_amountResourse];
        for (int i = 0; i < haves.Length; i++) haves[i] = true;

        haves[_numberFood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
        haves[_numberWood] = _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
        haves[_numberOre] = _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
        haves[_numberIron] = _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
        haves[_numberGold] = _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) >= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

        if (haves[_numberFood] && haves[_numberWood] && haves[_numberOre] && haves[_numberIron] && haves[_numberGold]) return true;
        else return false;
    }

    internal void MeltOre(Player player)
    {
        _eGM.EconomyEnt_EconomyCom.SetFood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, player.IsMasterClient) - StartValuesGameConfig.FOOD_FOR_MELTING_ORE);
        _eGM.EconomyEnt_EconomyCom.SetWood(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood,player.IsMasterClient) - StartValuesGameConfig.WOOD_FOR_MELTING_ORE);
        _eGM.EconomyEnt_EconomyCom.SetOre(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Ore(player.IsMasterClient) - StartValuesGameConfig.ORE_FOR_MELTING_ORE);
        _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) - StartValuesGameConfig.IRON_FOR_MELTING_ORE);
        _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) - StartValuesGameConfig.GOLD_FOR_MELTING_ORE);

        _eGM.EconomyEnt_EconomyCom.SetIron(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Iron(player.IsMasterClient) + 1);
        _eGM.EconomyEnt_EconomyCom.SetGold(player.IsMasterClient, _eGM.EconomyEnt_EconomyCom.Gold(player.IsMasterClient) + 1);
    }
}
