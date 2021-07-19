using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using UnityEngine;
using static Assets.Scripts.Main;
using static Assets.Scripts.Abstractions.ValuesConsts.EnvironmentValues;

internal struct CellEnvironmentComponent
{
    private bool _haveFertilizer;
    private bool _haveYoungTree;
    private bool _haveAdultTree;
    private bool _haveHill;
    private bool _haveMountain;
    private SpriteRenderer _fertilizerSR;
    private SpriteRenderer _youngTreeSR;
    private SpriteRenderer _adultTreeSR;
    private SpriteRenderer _hillSR;
    private SpriteRenderer _mountainSR;
    private int _amountFoodResources;
    private int _amountWoodResources;
    private int _amountOreResources;

    internal void StartFill(GameObject environmentGO)
    {
        _fertilizerSR = environmentGO.transform.Find("Food").GetComponent<SpriteRenderer>();
        _youngTreeSR = environmentGO.transform.Find("YoungTree").GetComponent<SpriteRenderer>();
        _adultTreeSR = environmentGO.transform.Find("Tree").GetComponent<SpriteRenderer>();
        _hillSR = environmentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();
        _mountainSR = environmentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();

        ResetAll();
    }

    internal void ResetAll()
    {
        _haveFertilizer = false;
        _haveYoungTree = false;
        _haveAdultTree = false;
        _haveHill = false;
        _haveMountain = false;

        _fertilizerSR.enabled = false;
        _youngTreeSR.enabled = false;
        _adultTreeSR.enabled = false;
        _hillSR.enabled = false;
        _mountainSR.enabled = false;


        _amountFoodResources = 0;
        _amountWoodResources = 0;
        _amountOreResources = 0;
    }

    internal bool HaveEnvironment(EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.None:
                throw new Exception();

            case EnvironmentTypes.Fertilizer:
                return _haveFertilizer;

            case EnvironmentTypes.YoungForest:
                return _haveYoungTree;

            case EnvironmentTypes.AdultForest:
                return _haveAdultTree;

            case EnvironmentTypes.Hill:
                return _haveHill;

            case EnvironmentTypes.Mountain:
                return _haveMountain;

            default:
                throw new Exception();
        }
    }

    internal bool HaveResources(ResourceTypes resourceType)
    {
        switch (resourceType)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                return _amountFoodResources > 0;

            case ResourceTypes.Wood:
                return _amountWoodResources > 0;

            case ResourceTypes.Ore:
                return _amountOreResources > 0;

            case ResourceTypes.Iron:
                throw new Exception();

            case ResourceTypes.Gold:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal int AmountResources(ResourceTypes resourceType)
    {
        switch (resourceType)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                return _amountFoodResources;

            case ResourceTypes.Wood:
                return _amountWoodResources;

            case ResourceTypes.Ore:
                return _amountOreResources;

            case ResourceTypes.Iron:
                throw new Exception();

            case ResourceTypes.Gold:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal void SetAmountResources(ResourceTypes resourceType, int value)
    {
        switch (resourceType)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                _amountFoodResources = value;
                break;

            case ResourceTypes.Wood:
                _amountWoodResources = value;
                break;

            case ResourceTypes.Ore:
                _amountOreResources = value;
                break;

            case ResourceTypes.Iron:
                throw new Exception();

            case ResourceTypes.Gold:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal void AddAmountResources(ResourceTypes resourceType, int adding = 1)
    {
        switch (resourceType)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                _amountFoodResources += adding;
                break;

            case ResourceTypes.Wood:
                _amountWoodResources += adding;
                break;

            case ResourceTypes.Ore:
                _amountOreResources += adding;
                break;

            case ResourceTypes.Iron:
                throw new Exception();

            case ResourceTypes.Gold:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal void TakeAmountResources(ResourceTypes resourceType, int taking = 1)
    {
        switch (resourceType)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                _amountFoodResources -= taking;
                break;

            case ResourceTypes.Wood:
                _amountWoodResources -= taking;
                break;

            case ResourceTypes.Ore:
                _amountOreResources -= taking;
                break;

            case ResourceTypes.Iron:
                throw new Exception();

            case ResourceTypes.Gold:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal int MaxAmountResources(EnvironmentTypes environmentTypes)
    {
        switch (environmentTypes)
        {
            case EnvironmentTypes.None:
                throw new Exception();

            case EnvironmentTypes.Fertilizer:
                return MAX_AMOUNT_FOOD;

            case EnvironmentTypes.YoungForest:
                throw new Exception();

            case EnvironmentTypes.AdultForest:
                return MAX_AMOUNT_WOOD;

            case EnvironmentTypes.Hill:
                return MAX_AMOUNT_ORE;

            case EnvironmentTypes.Mountain:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal int MinAmountResources(EnvironmentTypes environmentTypes)
    {
        switch (environmentTypes)
        {
            case EnvironmentTypes.None:
                throw new Exception();

            case EnvironmentTypes.Fertilizer:
                return MIN_AMOUNT_FOOD;

            case EnvironmentTypes.YoungForest:
                throw new Exception();

            case EnvironmentTypes.AdultForest:
                return MIN_AMOUNT_WOOD;

            case EnvironmentTypes.Hill:
                return MIN_AMOUNT_ORE;

            case EnvironmentTypes.Mountain:
                throw new Exception();

            default:
                throw new Exception();
        }
    }

    internal void SetNewEnvironment(EnvironmentTypes environmentType, params int[] xy)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.None:
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                _fertilizerSR.enabled = true;
                _amountFoodResources = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = true;
                _youngTreeSR.enabled = true;
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultTree = true;
                _adultTreeSR.enabled = true;
                _amountWoodResources = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                _hillSR.enabled = true;
                _amountOreResources = MaxAmountResources(environmentType);
                break;

            case EnvironmentTypes.Mountain:
                _haveMountain = true;
                _mountainSR.enabled = true;
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
                _mountainSR.enabled = true;
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultTree = true;
                _adultTreeSR.enabled = true;
                _amountWoodResources = amountEnvironmet;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = true;
                _youngTreeSR.enabled = true;
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                _hillSR.enabled = true;
                _amountOreResources = amountEnvironmet;
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                _fertilizerSR.enabled = true;
                _amountFoodResources = amountEnvironmet;
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
                _mountainSR.enabled = false;
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultTree = false;
                _adultTreeSR.enabled = false;
                _amountWoodResources = 0;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = false;
                _youngTreeSR.enabled = false;
                break;

            case EnvironmentTypes.Hill:
                _haveHill = false;
                _hillSR.enabled = false;
                break;

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = false;
                _fertilizerSR.enabled = false;
                _amountFoodResources = 0;
                break;

            default:
                break;
        }
    }

    internal int NeedAmountSteps()
    {
        int amountSteps = 1;

        if (HaveEnvironment(EnvironmentTypes.Fertilizer))
            amountSteps += UnitValues.NEED_AMOUNT_STEPS_FOOD;

        if (HaveEnvironment(EnvironmentTypes.AdultForest))
            amountSteps += UnitValues.NEED_AMOUNT_STEPS_TREE;

        if (HaveEnvironment(EnvironmentTypes.Hill))
            amountSteps += UnitValues.NEED_AMOUNT_STEPS_HILL;

        return amountSteps;
    }
}
