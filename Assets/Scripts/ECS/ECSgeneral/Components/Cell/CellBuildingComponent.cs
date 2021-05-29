using Photon.Realtime;
using UnityEngine;
using System;

internal struct CellBuildingComponent
{
    private GameObject _cityGO;
    private GameObject _farmGO;
    private GameObject _woodcutterGO;
    private GameObject _mineGO;
    private SpriteRenderer _citySR;
    private SpriteRenderer _farmSR;
    private SpriteRenderer _woodcutterSR;
    private SpriteRenderer _mineSR;

    internal CellBuildingComponent(ObjectPool gameObjectPool, int x, int y)
    {
        _cityGO = gameObjectPool.CellBuildingCityGOs[x, y];
        _farmGO = gameObjectPool.CellBuildingFarmGOs[x, y];
        _woodcutterGO = gameObjectPool.CellBuildingWoodcutterGOs[x, y];
        _mineGO = gameObjectPool.CellBuildingMineGOs[x, y];

        _citySR = _cityGO.GetComponent<SpriteRenderer>();
        _farmSR = _farmGO.GetComponent<SpriteRenderer>();
        _woodcutterSR = _woodcutterGO.GetComponent<SpriteRenderer>();
        _mineSR = _mineGO.GetComponent<SpriteRenderer>();
    }


    internal void SetColorBuilding(BuildingTypes buildingType, Player owner)
    {
        SpriteRenderer spriteRender;

        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                spriteRender = _citySR;
                break;

            case BuildingTypes.Farm:
                spriteRender = _farmSR;
                break;

            case BuildingTypes.Woodcutter:
                spriteRender = _woodcutterSR;
                break;

            case BuildingTypes.Mine:
                spriteRender = _mineSR;
                break;

            default:
                throw new Exception();
        }

        if (owner.IsMasterClient) spriteRender.color = Color.blue;
        else spriteRender.color = Color.red;
    }

    internal void SetEnabledSR(bool isActive, BuildingTypes buildingType)
    {
        switch (buildingType)
        {
            case BuildingTypes.None:
                throw new Exception();

            case BuildingTypes.City:
                _citySR.enabled = isActive;
                break;

            case BuildingTypes.Farm:
                _farmSR.enabled = isActive;
                break;

            case BuildingTypes.Woodcutter:
                _woodcutterSR.enabled = isActive;
                break;

            case BuildingTypes.Mine:
                _mineSR.enabled = isActive;
                break;

            default:
                throw new Exception();
        }
    }


}
