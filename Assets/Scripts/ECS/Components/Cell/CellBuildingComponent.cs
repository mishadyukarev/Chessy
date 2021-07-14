using Photon.Realtime;
using System;
using UnityEngine;

internal struct CellBuildingComponent
{
    private SpriteRenderer _citySR;
    private SpriteRenderer _cityBackSR;

    private SpriteRenderer _farmSR;
    private SpriteRenderer _farmBackSR;
    private SpriteRenderer _woodcutterSR;
    private SpriteRenderer _woodcutterBackSR;
    private SpriteRenderer _mineSR;
    private SpriteRenderer _mineBackSR;

    private int _timeStepsMine;

    internal void StartFill(GameObject buildingGO)
    {
        _citySR = buildingGO.transform.Find("City").GetComponent<SpriteRenderer>();
        _cityBackSR = _citySR.transform.Find("CityBack").GetComponent<SpriteRenderer>();
        _farmSR = buildingGO.transform.Find("Farm").GetComponent<SpriteRenderer>();
        _farmBackSR = _farmSR.transform.Find("FarmBack").GetComponent<SpriteRenderer>();
        _woodcutterSR = buildingGO.transform.Find("Woodcutter").GetComponent<SpriteRenderer>();
        _woodcutterBackSR = _woodcutterSR.transform.Find("WoodcutterBack").GetComponent<SpriteRenderer>();
        _mineSR = buildingGO.transform.Find("Mine").GetComponent<SpriteRenderer>();
        _mineBackSR = _mineSR.transform.Find("MineBack").GetComponent<SpriteRenderer>();
    }

    internal void EnabledPlayerSR(bool isActive, BuildingTypes buildingType, Player player = default)
    {
        if (buildingType == BuildingTypes.None) throw new Exception();

        var sR = GetSR(buildingType);
        sR.enabled = isActive;

        switch (buildingType)
        {
            case BuildingTypes.None:
                break;

            case BuildingTypes.City:
                sR = _cityBackSR;
                break;

            case BuildingTypes.Farm:
                sR = _farmBackSR;
                break;

            case BuildingTypes.Woodcutter:
                sR = _woodcutterBackSR;
                break;

            case BuildingTypes.Mine:
                sR = _mineBackSR;
                break;

            default:
                break;
        }

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

        switch (buildingType)
        {
            case BuildingTypes.None:
                break;

            case BuildingTypes.City:
                sR = _cityBackSR;
                break;

            case BuildingTypes.Farm:
                sR = _farmBackSR;
                break;

            case BuildingTypes.Woodcutter:
                sR = _woodcutterBackSR;
                break;

            case BuildingTypes.Mine:
                sR = _mineBackSR;
                break;

            default:
                break;
        }

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
