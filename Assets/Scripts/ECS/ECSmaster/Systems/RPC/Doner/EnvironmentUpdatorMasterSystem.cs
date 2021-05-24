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
                    if (!_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveTree && _eGM.CellEnt_CellCom(x, y).CellGO.activeSelf)
                    {
                        if (_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveFood)
                        {
                            random = Random.Range(0, 100);
                            if (random <= 50)
                            {
                                if (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType == BuildingTypes.Farm)
                                {
                                    _eGM.CellBuildingEnt_CellBuildingCom(x, y).ResetBuilding();
                                }
                                _eGM.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(false, EnvironmentTypes.Food);

                                isDeleted = true;
                                break;
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
