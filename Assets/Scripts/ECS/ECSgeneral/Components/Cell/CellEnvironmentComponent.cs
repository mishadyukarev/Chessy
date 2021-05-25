using System.Collections.Generic;
using UnityEngine;

public struct CellEnvironmentComponent
{
    private EntitiesGeneralManager _eGM;
    private int[] _xy;

    private bool _haveFood;
    private bool _haveMountain;
    private bool _haveAdultForest;
    private bool _haveYoungForest;
    private bool _haveHill;
    private GameObject _youngTreeGO;
    private GameObject _foodGO;
    private GameObject _mountainGO;
    private GameObject _treeGO;
    private GameObject _hillGO;

    internal int AmountResourcesForest;

    internal bool HaveFood => _haveFood;
    internal bool HaveMountain => _haveMountain;
    internal bool HaveAdultTree => _haveAdultForest;
    internal bool HaveYoungTree => _haveYoungForest;
    internal bool HaveHill => _haveHill;

    internal List<EnvironmentTypes> ListEnvironmentTypes
    {
        get
        {
            List<EnvironmentTypes> listEnvironmentTypes = new List<EnvironmentTypes>();

            if (_haveFood) listEnvironmentTypes.Add(EnvironmentTypes.Fertilizer);
            if (_haveAdultForest) listEnvironmentTypes.Add(EnvironmentTypes.AdultForest);
            if (_haveYoungForest) listEnvironmentTypes.Add(EnvironmentTypes.YoungForest);
            if (_haveHill) listEnvironmentTypes.Add(EnvironmentTypes.Hill);

            return listEnvironmentTypes;
        }
    }


    internal CellEnvironmentComponent(EntitiesGeneralManager eGM, GameObjectPool gameObjectPool, params int[] xy)
    {
        _eGM = eGM;
        _xy = xy;

        AmountResourcesForest = default;

        _haveYoungForest = false;
        _haveFood = false;
        _haveMountain = false;
        _haveAdultForest = false;
        _haveHill = false;

        
        _foodGO = gameObjectPool.CellEnvironmentFoodGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _mountainGO = gameObjectPool.CellEnvironmentMountainGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _treeGO = gameObjectPool.CellEnvironmentTreeGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _youngTreeGO = gameObjectPool.CellEnvironmentYoungTreeGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _hillGO = gameObjectPool.CellEnvironmentHillGOs[_xy[_eGM.X], _xy[_eGM.Y]];
    }


    internal void SetResetEnvironment(bool isActive, EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.Mountain:
                _haveMountain = isActive;
                _mountainGO.SetActive(isActive);
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultForest = isActive;
                _treeGO.SetActive(isActive);
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungForest = isActive;
                _youngTreeGO.SetActive(isActive);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = isActive;
                _hillGO.SetActive(isActive);
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFood = isActive;
                _foodGO.SetActive(isActive);
                break;

            default:
                break;
        }
    }

    internal void SetDefaultAmountResources(EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.Mountain:
                break;

            case EnvironmentTypes.AdultForest:
                AmountResourcesForest = Random.Range(30, 50);
                break;

            case EnvironmentTypes.YoungForest:
                break;

            case EnvironmentTypes.Hill:
                break;

            case EnvironmentTypes.Fertilizer:
                break;

            default:
                break;
        }
    }
}
