using System;

internal struct CellBuildingComponent
{
    private int _timeStepsMine;

    internal void StartFill()
    {
        _timeStepsMine = default;
    }

    internal int TimeStepsBuilding(BuildingTypes buildingType)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                throw new Exception();

            case BuildingTypes.Woodcutter:
                throw new Exception();

            case BuildingTypes.Mine:
                return _timeStepsMine;

            default:
                throw new Exception();
        }
    }

    internal int SetTimeStepsBuilding(BuildingTypes buildingType, int value)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                throw new Exception();

            case BuildingTypes.Woodcutter:
                throw new Exception();

            case BuildingTypes.Mine:
                return _timeStepsMine = value;

            default:
                throw new Exception();
        }
    }

    internal int AddTimeStepsBuilding(BuildingTypes buildingType, int adding = 1)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                throw new Exception();

            case BuildingTypes.Woodcutter:
                throw new Exception();

            case BuildingTypes.Mine:
                return _timeStepsMine += adding;

            default:
                throw new Exception();
        }
    }

    internal int TakeTimeStepsBuilding(BuildingTypes buildingType, int taking = 1)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                throw new Exception();

            case BuildingTypes.Farm:
                throw new Exception();

            case BuildingTypes.Woodcutter:
                throw new Exception();

            case BuildingTypes.Mine:
                return _timeStepsMine -= taking;

            default:
                throw new Exception();
        }
    }
}
