using UnityEngine;

internal sealed class FertilizeUpdatorMasterSystem : SystemGeneralReduction
{
    private int _steps;

    internal FertilizeUpdatorMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }

    public override void Run()
    {
        base.Run();

        _steps += 1;
        var canRemoveFarm = _steps >= Random.Range(2,3);

        if (canRemoveFarm)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellEnt_CellCom(x, y).IsActiveSelf)
                    {
                        if (_eGM.CellEnt_CellEnvCom(x, y).HaveFertilizer)
                        {
                            if (Random.Range(0, 100) <= 80)
                            {
                                _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(false, EnvironmentTypes.Fertilizer);

                                if (_eGM.CellEnt_CellBuildingCom(x, y).BuildingType == BuildingTypes.Farm)
                                {
                                    _eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                    _eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                    _eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                    _eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                                    _eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] += StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                                    _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[_eGM.CellEnt_CellBuildingCom(x, y).Owner.IsMasterClient] -= 1;

                                    _eGM.CellEnt_CellBuildingCom(x, y).ResetBuilding();

                                }
                                _steps = 0;
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
