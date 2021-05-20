using UnityEngine;

public struct CellSupportVisionComponent
{
    private EntitiesGeneralManager _eGM;

    private int[] _xy;
    private GameObject _selectorGO;
    private GameObject _spawnGO;
    private GameObject _wayUnitGO;
    private GameObject _enemyGO;
    private GameObject _uniqueAttackGO;
    private GameObject _zoneGO;

    internal CellSupportVisionComponent( EntitiesGeneralManager eGM, GameObjectPool gameObjectPool, params int[] xy)
    {
        _eGM = eGM;


        _xy = xy;

        _selectorGO = gameObjectPool.CellSupportVisionSelectorGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _spawnGO = gameObjectPool.CellSupportVisionSpawnGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _wayUnitGO = gameObjectPool.CellSupportVisionWayUnitGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _enemyGO = gameObjectPool.CellSupportVisionEnemyGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _uniqueAttackGO = gameObjectPool.CellSupportVisionUniqueAttackGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _zoneGO = gameObjectPool.CellSupportVisionZoneGOs[_xy[_eGM.X], _xy[_eGM.Y]];
    }


    internal void ActiveVision(bool isActive, SupportVisionTypes supportVisionType)
    {
        switch (supportVisionType)
        {
            case SupportVisionTypes.None:
                break;

            case SupportVisionTypes.Selector:
                _selectorGO.SetActive(isActive);
                break;

            case SupportVisionTypes.Spawn:
                _spawnGO.SetActive(isActive);
                break;

            case SupportVisionTypes.WayOfUnit:
                _wayUnitGO.SetActive(isActive);
                break;

            case SupportVisionTypes.SimpleAttack:
                _enemyGO.SetActive(isActive);
                break;

            case SupportVisionTypes.UniqueAttack:
                _uniqueAttackGO.SetActive(isActive);
                break;

            case SupportVisionTypes.Zone:
                //_zoneGO.SetActive(isActive);
                //SetColorVision(_zoneGO.GetComponent<SpriteRenderer>(), _player);
                break;

            default:
                break;
        }
    }
}
