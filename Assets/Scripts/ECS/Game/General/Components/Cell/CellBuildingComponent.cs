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

    internal void Fill(GameObject buildingGO)
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

    internal void SetEnabledSR(bool isActive, BuildingTypes buildingType, Player player = default)
    {
        SpriteRenderer spriteRender;

        switch (buildingType)
        {
            case BuildingTypes.None:
                return;

            case BuildingTypes.City:
                spriteRender = _citySR;
                spriteRender.enabled = isActive;
                break;

            case BuildingTypes.Farm:
                spriteRender = _farmSR;
                spriteRender.enabled = isActive;
                break;

            case BuildingTypes.Woodcutter:
                spriteRender = _woodcutterSR;
                spriteRender.enabled = isActive;
                break;

            case BuildingTypes.Mine:
                spriteRender = _mineSR;
                spriteRender.enabled = isActive;
                break;

            default:
                throw new Exception();
        }

        if (player != default)
        {
            if (player.IsMasterClient) spriteRender.color = Color.blue;
            else spriteRender.color = Color.red;
        }
    }
}
