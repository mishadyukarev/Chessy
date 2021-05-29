using UnityEngine;

internal struct CellSupportVisionComponent
{
    private GameObject _selectorGO;
    private GameObject _spawnGO;
    private GameObject _wayUnitGO;
    private GameObject _enemyGO;
    private GameObject _uniqueAttackGO;
    private GameObject _zoneGO;

    internal CellSupportVisionComponent(ObjectPool gameObjectPool, int x, int y)
    {
        _selectorGO = gameObjectPool.CellSupportVisionSelectorGOs[x, y];
        _spawnGO = gameObjectPool.CellSupportVisionSpawnGOs[x, y];
        _wayUnitGO = gameObjectPool.CellSupportVisionWayUnitGOs[x, y];
        _enemyGO = gameObjectPool.CellSupportVisionEnemyGOs[x, y];
        _uniqueAttackGO = gameObjectPool.CellSupportVisionUniqueAttackGOs[x, y];
        _zoneGO = gameObjectPool.CellSupportVisionZoneGOs[x, y];
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
                break;

            default:
                break;
        }
    }
}
