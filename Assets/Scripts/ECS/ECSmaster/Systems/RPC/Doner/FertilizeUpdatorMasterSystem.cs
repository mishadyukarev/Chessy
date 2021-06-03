﻿using UnityEngine;

internal sealed class FertilizeUpdatorMasterSystem : SystemGeneralReduction
{
    private int _steps;
    private int _randomFor;
    private int _standartRandom => Random.Range(0, 100);
    private int _randomFor2 => Random.Range(2, 4);

    internal FertilizeUpdatorMasterSystem()
    {
        _randomFor = _randomFor2;
    }

    public override void Run()
    {
        base.Run();

        _steps += 1;
        var canRemoveFarm = _steps >= _randomFor;

        if (canRemoveFarm)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellEnt_CellBaseCom(x, y).IsActiveSelfGO)
                    {
                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer)
                        {
                            if (_standartRandom <= 80)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.Fertilizer);

                                if (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType == BuildingTypes.Farm)
                                {
                                    _eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient] += _startValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                    _eGM.WoodEAmountDictC.AmountDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient] += _startValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                    _eGM.OreEAmountDictC.AmountDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient] += _startValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                    _eGM.IronEAmountDictC.AmountDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient] += _startValuesGameConfig.IRON_FOR_BUILDING_FARM;
                                    _eGM.GoldEAmountDictC.AmountDict[_eGM.CellBuildingEnt_OwnerCom(x, y).IsMasterClient] += _startValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                                    _cM.CellBuildingWorker.ResetBuilding(x, y);
                                }
                                _steps = 0;
                                _randomFor = _randomFor2;
                                break;
                            }
                        }
                    }
                    if (_steps == 0) break;
                }
            }
        }
    }
}
