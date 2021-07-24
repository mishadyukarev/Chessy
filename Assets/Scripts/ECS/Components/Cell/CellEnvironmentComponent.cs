using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using UnityEngine;
using static Assets.Scripts.Main;
using static Assets.Scripts.Abstractions.ValuesConsts.EnvironmentValues;

internal struct CellEnvironmentComponent
{
    private bool _haveFertilizer;
    private bool _haveYoungForest;
    private bool _haveAdultForest;
    private bool _haveHill;
    private bool _haveMountain;
    private SpriteRenderer _fertilizerSR;
    private SpriteRenderer _youngForestSR;
    private SpriteRenderer _adultForestSR;
    private SpriteRenderer _hillSR;
    private SpriteRenderer _mountainSR;
    private int _amountFoodResources;
    private int _amountWoodResources;
    private int _amountOreResources;

    internal void StartFill(GameObject environmentGO)
    {
        _fertilizerSR = environmentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
        _youngForestSR = environmentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();
        _adultForestSR = environmentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();
        _hillSR = environmentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();
        _mountainSR = environmentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();

        ResetAll();
    }

    internal void ResetAll()
    {
        _haveFertilizer = false;
        _haveYoungForest = false;
        _haveAdultForest = false;
        _haveHill = false;
        _haveMountain = false;

        _fertilizerSR.enabled = false;
        _youngForestSR.enabled = false;
        _adultForestSR.enabled = false;
        _hillSR.enabled = false;
        _mountainSR.enabled = false;


        _amountFoodResources = 0;
        _amountWoodResources = 0;
        _amountOreResources = 0;
    }

    //internal bool HaveEnvironment(EnvironmentTypes environmentType)
    //{
    //    switch (environmentType)
    //    {
    //        case EnvironmentTypes.None:
    //            throw new Exception();

    //        case EnvironmentTypes.Fertilizer:
    //            return _haveFertilizer;

    //        case EnvironmentTypes.YoungForest:
    //            return _haveYoungForest;

    //        case EnvironmentTypes.AdultForest:
    //            return _haveAdultForest;

    //        case EnvironmentTypes.Hill:
    //            return _haveHill;

    //        case EnvironmentTypes.Mountain:
    //            return _haveMountain;

    //        default:
    //            throw new Exception();
    //    }
    //}

    //internal int MaxAmountResources(EnvironmentTypes environmentTypes)
    //{
    //    switch (environmentTypes)
    //    {
    //        case EnvironmentTypes.None:
    //            throw new Exception();

    //        case EnvironmentTypes.Fertilizer:
    //            return MAX_AMOUNT_FOOD;

    //        case EnvironmentTypes.YoungForest:
    //            throw new Exception();

    //        case EnvironmentTypes.AdultForest:
    //            return MAX_AMOUNT_WOOD;

    //        case EnvironmentTypes.Hill:
    //            return MAX_AMOUNT_ORE;

    //        case EnvironmentTypes.Mountain:
    //            throw new Exception();

    //        default:
    //            throw new Exception();
    //    }
    //}

    //internal int MinAmountResources(EnvironmentTypes environmentTypes)
    //{
    //    switch (environmentTypes)
    //    {
    //        case EnvironmentTypes.None:
    //            throw new Exception();

    //        case EnvironmentTypes.Fertilizer:
    //            return MIN_AMOUNT_FOOD;

    //        case EnvironmentTypes.YoungForest:
    //            throw new Exception();

    //        case EnvironmentTypes.AdultForest:
    //            return MIN_AMOUNT_WOOD;

    //        case EnvironmentTypes.Hill:
    //            return MIN_AMOUNT_ORE;

    //        case EnvironmentTypes.Mountain:
    //            throw new Exception();

    //        default:
    //            throw new Exception();
    //    }
    //}

    //internal void SetNewEnvironment(EnvironmentTypes environmentType, params int[] xy)
    //{
    //    switch (environmentType)
    //    {
    //        case EnvironmentTypes.None:
    //            break;

    //        case EnvironmentTypes.Fertilizer:
    //            _haveFertilizer = true;
    //            _fertilizerSR.enabled = true;
    //            _amountFoodResources = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
    //            break;

    //        case EnvironmentTypes.YoungForest:
    //            _haveYoungForest = true;
    //            _youngForestSR.enabled = true;
    //            break;

    //        case EnvironmentTypes.AdultForest:
    //            _haveAdultForest = true;
    //            _adultForestSR.enabled = true;
    //            _amountWoodResources = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
    //            break;

    //        case EnvironmentTypes.Hill:
    //            _haveHill = true;
    //            _hillSR.enabled = true;
    //            _amountOreResources = MaxAmountResources(environmentType);
    //            break;

    //        case EnvironmentTypes.Mountain:
    //            _haveMountain = true;
    //            _mountainSR.enabled = true;
    //            break;

    //        default:
    //            break;
    //    }
    //}
    //internal void SetEnvironment(EnvironmentTypes environmentType, int amountEnvironmet)
    //{
    //    switch (environmentType)
    //    {
    //        case EnvironmentTypes.None:
    //            break;

    //        case EnvironmentTypes.Mountain:
    //            _haveMountain = true;
    //            _mountainSR.enabled = true;
    //            break;

    //        case EnvironmentTypes.AdultForest:
    //            _haveAdultForest = true;
    //            _adultForestSR.enabled = true;
    //            _amountWoodResources = amountEnvironmet;
    //            break;

    //        case EnvironmentTypes.YoungForest:
    //            _haveYoungForest = true;
    //            _youngForestSR.enabled = true;
    //            break;

    //        case EnvironmentTypes.Hill:
    //            _haveHill = true;
    //            _hillSR.enabled = true;
    //            _amountOreResources = amountEnvironmet;
    //            break;

    //        case EnvironmentTypes.Fertilizer:
    //            _haveFertilizer = true;
    //            _fertilizerSR.enabled = true;
    //            _amountFoodResources = amountEnvironmet;
    //            break;

    //        default:
    //            break;
    //    }
    //}
    //internal void ResetEnvironment(EnvironmentTypes environmentType)
    //{
    //    switch (environmentType)
    //    {
    //        case EnvironmentTypes.None:
    //            break;

    //        case EnvironmentTypes.Mountain:
    //            _haveMountain = false;
    //            _mountainSR.enabled = false;
    //            break;

    //        case EnvironmentTypes.AdultForest:
    //            _haveAdultForest = false;
    //            _adultForestSR.enabled = false;
    //            _amountWoodResources = 0;
    //            break;

    //        case EnvironmentTypes.YoungForest:
    //            _haveYoungForest = false;
    //            _youngForestSR.enabled = false;
    //            break;

    //        case EnvironmentTypes.Hill:
    //            _haveHill = false;
    //            _hillSR.enabled = false;
    //            break;

    //        case EnvironmentTypes.Fertilizer:
    //            _haveFertilizer = false;
    //            _fertilizerSR.enabled = false;
    //            _amountFoodResources = 0;
    //            break;

    //        default:
    //            break;
    //    }
    //}

    //internal int NeedAmountSteps()
    //{
    //    int amountSteps = 1;

    //    if (HaveEnvironment(EnvironmentTypes.Fertilizer))
    //        amountSteps += UnitValues.NEED_AMOUNT_STEPS_FOOD;

    //    if (HaveEnvironment(EnvironmentTypes.AdultForest))
    //        amountSteps += UnitValues.NEED_AMOUNT_STEPS_TREE;

    //    if (HaveEnvironment(EnvironmentTypes.Hill))
    //        amountSteps += UnitValues.NEED_AMOUNT_STEPS_HILL;

    //    return amountSteps;
    //}
}
