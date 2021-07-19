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

    private int _amountFoodResources;
    private int _amountWoodResources;
    private int _amountOreResources;

    internal void StartFill()
    {
        ResetAll();
    }

    internal void ResetAll()
    {
        _haveFertilizer = default;
        _haveYoungTree = default;
        _haveAdultTree = default;
        _haveHill = default;
        _haveMountain = default;

        _amountFoodResources = default;
        _amountWoodResources = default;
        _amountOreResources = default;
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
    internal void AddEnvironment(EnvironmentTypes environmentType)
    {
        switch (environmentType)
        {
            case EnvironmentTypes.None:
                throw new Exception();

            case EnvironmentTypes.Fertilizer:
                _haveFertilizer = true;
                break;

            case EnvironmentTypes.YoungForest:
                _haveYoungTree = true;
                break;

            case EnvironmentTypes.AdultForest:
                _haveAdultTree = true;
                break;

            case EnvironmentTypes.Hill:
                _haveHill = true;
                break;

            case EnvironmentTypes.Mountain:
                _haveMountain = true;
                break;

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
