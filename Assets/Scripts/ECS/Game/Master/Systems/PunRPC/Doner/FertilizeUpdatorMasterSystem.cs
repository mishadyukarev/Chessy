using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Static;
using UnityEngine;
using static Assets.Scripts.Main;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellWorker;

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
                    var xy = new int[] { x, y };

                    if (IsActiveSelfGO(xy))
                    {
                        if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        {
                            if (_standartRandom <= 80)
                            {
                                ResetEnvironment(EnvironmentTypes.Fertilizer, xy);

                                if (_eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType == BuildingTypes.Farm)
                                {
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, EconomyValues.FOOD_FOR_BUILDING_FARM);
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, EconomyValues.WOOD_FOR_BUILDING_FARM);
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Ore, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, EconomyValues.ORE_FOR_BUILDING_FARM);
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Iron, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, EconomyValues.IRON_FOR_BUILDING_FARM);
                                    _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Gold, _eGM.CellBuildEnt_OwnerCom(x, y).IsMasterClient, EconomyValues.GOLD_FOR_BUILDING_FARM);

                                    CellBuildingWorker.ResetBuilding(true, x, y);
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
