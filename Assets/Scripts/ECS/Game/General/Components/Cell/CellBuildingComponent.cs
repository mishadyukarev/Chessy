using Photon.Realtime;
using System;
using UnityEngine;

internal struct CellBuildingComponent
{
    private GameObject _cityParentGO;
    private GameObject _farmGO;
    private GameObject _woodcutterGO;
    private GameObject _mineGO;
    private SpriteRenderer _citySR;
    private SpriteRenderer _farmSR;
    private SpriteRenderer _woodcutterSR;
    private SpriteRenderer _mineSR;

    internal void StartFill(GameObject buildingGO)
    {
        _cityParentGO = buildingGO.transform.Find("City").gameObject;
        _farmGO = buildingGO.transform.Find("Farm").gameObject;
        _woodcutterGO = buildingGO.transform.Find("Woodcutter").gameObject;
        _mineGO = buildingGO.transform.Find("Mine").gameObject;

        _citySR = _cityParentGO.GetComponent<SpriteRenderer>();
        _farmSR = _farmGO.GetComponent<SpriteRenderer>();
        _woodcutterSR = _woodcutterGO.GetComponent<SpriteRenderer>();
        _mineSR = _mineGO.GetComponent<SpriteRenderer>();
    }

    internal void EnabledPlayerSR(bool isActive, BuildingTypes buildingType, Player player = default)
    {
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
        var sR = GetSR(buildingType);
        sR.enabled = isActive;

        if (isActive) sR.color = Color.red;
    }

    private SpriteRenderer GetSR(BuildingTypes buildingType)
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
}
