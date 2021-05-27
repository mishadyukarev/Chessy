using Photon.Realtime;
using UnityEngine;

public struct CellBuildingComponent
{
    private EntitiesGeneralManager _eGM;
    private int[] _xy;

    internal BuildingTypes BuildingType;

    internal bool HaveBuilding => BuildingType != BuildingTypes.None;
    internal Player Owner;
    private GameObject _cityGO;
    private GameObject _farmGO;
    private GameObject _woodcutterGO;
    private GameObject _mineGO;

    internal CellBuildingComponent(EntitiesGeneralManager eGM, GameObjectPool gameObjectPool,  params int[] xy)
    {
        _eGM = eGM;

        BuildingType = default;
        Owner = default;
        _xy = xy;
        _cityGO = gameObjectPool.CellBuildingCityGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _farmGO = gameObjectPool.CellBuildingFarmGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _woodcutterGO = gameObjectPool.CellBuildingWoodcutterGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _mineGO = gameObjectPool.CellBuildingMineGOs[_xy[_eGM.X], _xy[_eGM.Y]];
    }


    private void SetColorBuilding(in SpriteRenderer unitSpriteRender, in Player player)
    {
        if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
        else unitSpriteRender.color = Color.red;
    }

    internal void SetBuilding(BuildingTypes buildingType, Player player)
    {
        BuildingType = buildingType;
        Owner = player;

        _cityGO.SetActive(false);
        _farmGO.SetActive(false);
        _woodcutterGO.SetActive(false);
        _mineGO.SetActive(false);

        switch (buildingType)
        {
            case BuildingTypes.City:
                _cityGO.SetActive(true);
                SetColorBuilding(_cityGO.GetComponent<SpriteRenderer>(), player);
                break;

            case BuildingTypes.Farm:
                _farmGO.SetActive(true);
                SetColorBuilding(_farmGO.GetComponent<SpriteRenderer>(), player);
                break;

            case BuildingTypes.Woodcutter:
                _woodcutterGO.SetActive(true);
                SetColorBuilding(_woodcutterGO.GetComponent<SpriteRenderer>(), player);
                break;

            case BuildingTypes.Mine:
                _mineGO.SetActive(true);
                SetColorBuilding(_mineGO.GetComponent<SpriteRenderer>(), player);
                break;
        }
    }
    internal void ResetBuilding()
    {
        var buildingType = BuildingTypes.None;
        Player player = default;
        SetBuilding(buildingType, player);
    }



    private bool _haveOwner => Owner != default;

    internal int ActorNumber
    {
        get
        {
            if (_haveOwner) return Owner.ActorNumber;
            else return -1;
        }
    }

    internal bool IsMine
    {
        get
        {
            if (_haveOwner) return Owner.IsLocal;
            else return default;
        }
    }

    internal bool IsMasterClient
    {
        get
        {
            if (_haveOwner) return Owner.IsMasterClient;
            else return default;
        }
    }


    internal bool IsHim(Player player)
    {
        if (player == default) return default;
        return player.ActorNumber == Owner.ActorNumber;
    }
}
