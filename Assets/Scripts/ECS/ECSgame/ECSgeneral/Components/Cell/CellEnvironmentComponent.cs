using System;
using System.Collections.Generic;
using UnityEngine;

internal struct CellEnvironmentComponent
{
    internal bool _haveFertilizer;
    internal bool _haveMountain;
    internal bool _haveAdultTree;
    internal bool _haveYoungTree;
    internal bool _haveHill;
    private GameObject _youngTreeGO;
    private GameObject _fertilizerGO;
    private GameObject _mountainGO;
    private GameObject _adultTreeGO;
    private GameObject _hillGO;
    private SpriteRenderer _youngTreeSR;
    private SpriteRenderer _fertilizerSR;
    private SpriteRenderer _mountainSR;
    private SpriteRenderer _adultTreeSR;
    private SpriteRenderer _hillSR;

    internal int AmountFertilizerResources;
    internal int AmountForestResources;
    internal int AmountOreResources;

    internal int MineStep;

    internal bool HaveFertilizer => _haveFertilizer;
    internal bool HaveMountain => _haveMountain;
    internal bool HaveAdultTree => _haveAdultTree;
    internal bool HaveYoungTree => _haveYoungTree;
    internal bool HaveHill => _haveHill;


    internal bool HaveFertilizerResources => AmountFertilizerResources > 0;
    internal bool HaveForestResources => AmountForestResources > 0;
    internal bool HaveOreResources => AmountOreResources > 0;

    internal List<EnvironmentTypes> ListEnvironmentTypes
    {
        get
        {
            List<EnvironmentTypes> listEnvironmentTypes = new List<EnvironmentTypes>();

            if (_haveFertilizer) listEnvironmentTypes.Add(EnvironmentTypes.Fertilizer);
            if (_haveAdultTree) listEnvironmentTypes.Add(EnvironmentTypes.AdultForest);
            if (_haveYoungTree) listEnvironmentTypes.Add(EnvironmentTypes.YoungForest);
            if (_haveHill) listEnvironmentTypes.Add(EnvironmentTypes.Hill);

            return listEnvironmentTypes;
        }
    }

    internal void SetNewEnvironment(EnvironmentTypes environmentType, params int[] xy)
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
                _haveAdultTree = true;
                _adultTreeGO.SetActive(true);
                AmountForestResources = UnityEngine.Random.Range(15, 20);
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = true;
                _youngTreeGO.SetActive(true);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                _hillGO.SetActive(true);
                AmountOreResources = UnityEngine.Random.Range(50, 60);
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                _fertilizerGO.SetActive(true);
                AmountFertilizerResources = UnityEngine.Random.Range(15, 20);
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
                _haveAdultTree = true;
                _adultTreeGO.SetActive(true);
                AmountForestResources = amountEnvironmet;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = true;
                _youngTreeGO.SetActive(true);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                _hillGO.SetActive(true);
                AmountOreResources = amountEnvironmet;
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                _fertilizerGO.SetActive(true);
                AmountFertilizerResources = amountEnvironmet;
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
                _haveAdultTree = false;
                _adultTreeGO.SetActive(false);
                AmountForestResources = 0;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = false;
                _youngTreeGO.SetActive(false);
                break;

            case EnvironmentTypes.Hill:
                _haveHill = false;
                _hillGO.SetActive(false);
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = false;
                _fertilizerGO.SetActive(false);
                AmountFertilizerResources = 0;
                break;

            default:
                break;
        }
    }

    internal void Fill(GameObject environmentGO)
    {
        _fertilizerGO = environmentGO.transform.Find("Food").gameObject;
        _mountainGO = environmentGO.transform.Find("Mountain").gameObject;
        _adultTreeGO = environmentGO.transform.Find("Tree").gameObject;
        _youngTreeGO = environmentGO.transform.Find("YoungTree").gameObject;
        _hillGO = environmentGO.transform.Find("Hill").gameObject;

        _fertilizerSR = _fertilizerGO.GetComponent<SpriteRenderer>();
        _mountainSR = _mountainGO.GetComponent<SpriteRenderer>();
        _adultTreeSR = _adultTreeGO.GetComponent<SpriteRenderer>();
        _youngTreeSR = _youngTreeGO.GetComponent<SpriteRenderer>();
        _hillSR = _hillGO.GetComponent<SpriteRenderer>();

    }
}
