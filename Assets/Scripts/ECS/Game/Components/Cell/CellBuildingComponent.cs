using Photon.Realtime;
using System;
using UnityEngine;

internal struct CellBuildingComponent
{
    private SpriteRenderer _citySR;
    private SpriteRenderer _farmSR;
    private SpriteRenderer _woodcutterSR;
    private SpriteRenderer _mineSR;
    private int _timeStepsMine;

    internal void StartFill(GameObject buildingGO)
    {
        _citySR = buildingGO.transform.Find("City").GetComponent<SpriteRenderer>();
        _farmSR = buildingGO.transform.Find("Farm").GetComponent<SpriteRenderer>();
        _woodcutterSR = buildingGO.transform.Find("Woodcutter").GetComponent<SpriteRenderer>();
        _mineSR = buildingGO.transform.Find("Mine").GetComponent<SpriteRenderer>();
    }

    internal void EnabledPlayerSR(bool isActive, BuildingTypes buildingType, Player player = default)
    {
        if (buildingType == BuildingTypes.None) throw new Exception();

        var sR = GetSR(buildingType);
        sR.enabled = isActive;

        if (player != default)
        {
            if (player.IsMasterClient) sR.color = Color.blue;
            else sR.color = Color.red;
        }
    }

    internal void EnabledBotSR(bool isActive, BuildingTypes buildingType)
    {
        if (buildingType == BuildingTypes.None) throw new Exception();

        var sR = GetSR(buildingType);
        sR.enabled = isActive;

        if (isActive) sR.color = Color.red;
    }

    internal SpriteRenderer GetSR(BuildingTypes buildingType)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                return _citySR;

            case BuildingTypes.Farm:
                return _farmSR;

            case BuildingTypes.Woodcutter:
                return _woodcutterSR;

            case BuildingTypes.Mine:
                return _mineSR;

            default:
                throw new Exception();
        }
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
