using System.Collections.Generic;
using UnityEngine;

internal struct CellEnvironmentComponent
{
    private EntitiesGeneralManager _eGM;

    private bool _haveFertilizer;
    private bool _haveMountain;
    private bool _haveAdultForest;
    private bool _haveYoungForest;
    private bool _haveHill;
    private GameObject _youngTreeGO;
    private GameObject _fertilizerGO;
    private GameObject _mountainGO;
    private GameObject _treeGO;
    private GameObject _hillGO;

    internal int AmountFertilizer;
    internal int AmountForest;
    internal bool HaveFertilizerResources => AmountFertilizer <= 0;
    internal bool HaveForestResources => AmountForest <= 0;

    internal bool HaveFertilizer => _haveFertilizer;
    internal bool HaveMountain => _haveMountain;
    internal bool HaveAdultTree => _haveAdultForest;
    internal bool HaveYoungTree => _haveYoungForest;
    internal bool HaveHill => _haveHill;

    internal List<EnvironmentTypes> ListEnvironmentTypes
    {
        get
        {
            List<EnvironmentTypes> listEnvironmentTypes = new List<EnvironmentTypes>();

            if (_haveFertilizer) listEnvironmentTypes.Add(EnvironmentTypes.Fertilizer);
            if (_haveAdultForest) listEnvironmentTypes.Add(EnvironmentTypes.AdultForest);
            if (_haveYoungForest) listEnvironmentTypes.Add(EnvironmentTypes.YoungForest);
            if (_haveHill) listEnvironmentTypes.Add(EnvironmentTypes.Hill);

            return listEnvironmentTypes;
        }
    }


    internal CellEnvironmentComponent(EntitiesGeneralManager eGM, ObjectPool gameObjectPool, int x, int y)
    {
        _eGM = eGM;

        AmountFertilizer = default;
        AmountForest = default;

        _haveFertilizer = false;
        _haveYoungForest = false;
        _haveAdultForest = false;
        _haveMountain = false;
        _haveHill = false;
        
        _fertilizerGO = gameObjectPool.CellEnvironmentFoodGOs[x, y];
        _mountainGO = gameObjectPool.CellEnvironmentMountainGOs[x, y];
        _treeGO = gameObjectPool.CellEnvironmentTreeGOs[x, y];
        _youngTreeGO = gameObjectPool.CellEnvironmentYoungTreeGOs[x, y];
        _hillGO = gameObjectPool.CellEnvironmentHillGOs[x, y];
    }

    internal void SetNewEnvironment(EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.None:
                break;

            case EnvironmentTypes.Mountain:
                _haveMountain = true;
                _mountainGO.SetActive(true);
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultForest = true;
                _treeGO.SetActive(true);
                AmountForest = Random.Range(15, 20);
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungForest = true;
                _youngTreeGO.SetActive(true);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                _hillGO.SetActive(true);
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                _fertilizerGO.SetActive(true);
                AmountFertilizer = Random.Range(15, 20);
                break;

            default:
                break;
        }
    }
    internal void SetEnvironment(EnvironmentTypes environmentType, int amountEnvironmet)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.None:
                break;

            case EnvironmentTypes.Mountain:
                _haveMountain = true;
                _mountainGO.SetActive(true);
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultForest = true;
                _treeGO.SetActive(true);
                AmountForest = amountEnvironmet;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungForest = true;
                _youngTreeGO.SetActive(true);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                _hillGO.SetActive(true);
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                _fertilizerGO.SetActive(true);
                break;

            default:
                break;
        }
    }
    internal void ResetEnvironment(EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.None:
                break;

            case EnvironmentTypes.Mountain:
                _haveMountain = false;
                _mountainGO.SetActive(false);
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultForest = false;
                _treeGO.SetActive(false);
                AmountForest = 0;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungForest = false;
                _youngTreeGO.SetActive(false);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = false;
                _hillGO.SetActive(false);
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = false;
                _fertilizerGO.SetActive(false);
                AmountFertilizer = 0;
                break;

            default:
                break;
        }
    }
}
