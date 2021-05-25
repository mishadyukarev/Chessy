using UnityEngine;
using static MainGame;

internal class EnvironmentUpdatorMasterSystem : SystemGeneralReduction
{
    private int _minMotions = 5;
    private int _maxMotions = 20;
    private int _numberUpdateFood;

    internal EnvironmentUpdatorMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();

        int random = Random.Range(_minMotions, _maxMotions);

        _numberUpdateFood += 1;
        if (_numberUpdateFood >= random)
        {
            bool isDeleted = false;
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (!_eGM.CellEnt_CellEnvCom(x, y).HaveAdultTree)
                    {
                        if(_eGM.CellEnt_CellCom(x, y).IsActiveSelf)
                        {
                            if (_eGM.CellEnt_CellEnvCom(x, y).HaveFood)
                            {
                                random = Random.Range(0, 100);
                                if (random <= 50)
                                {
                                    if (_eGM.CellEnt_CellBuildingCom(x, y).BuildingType == BuildingTypes.Farm)
                                    {
                                        _eGM.CellEnt_CellBuildingCom(x, y).ResetBuilding();
                                    }
                                    _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(false, EnvironmentTypes.Fertilizer);

                                    isDeleted = true;
                                    break;
                                }
                            }
                        }
                    }

                    else
                    {
                        if (_eGM.CellEnt_CellBuildingCom(x, y).BuildingType == BuildingTypes.Woodcutter)
                        {
                            _eGM.CellEnt_CellEnvCom(x, y).AmountResourcesForest -= (int)(Instance.StartValuesGameConfig.BENEFIT_WOOD_WOODCUTTER + (0.25f * _eGM.InfoEnt_UpgradeCom.AmountUpgradeWoodcutterDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterClient]));
                            if (_eGM.CellEnt_CellEnvCom(x, y).AmountResourcesForest <= 0)
                            {
                                _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(false, EnvironmentTypes.AdultForest);
                                _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterClient] -= 1;
                                _eGM.CellEnt_CellBuildingCom(x, y).ResetBuilding();
                            }
                        }
                    }
                }
                if (isDeleted) break;
            }

            _numberUpdateFood = 0;
        }
    }
}
