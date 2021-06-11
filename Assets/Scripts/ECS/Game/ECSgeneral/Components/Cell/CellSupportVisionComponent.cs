using UnityEngine;

internal struct CellSupportVisionComponent
{
    private GameObject _selectorGO;
    private GameObject _spawnGO;
    private GameObject _wayUnitGO;
    private GameObject _enemyGO;
    private GameObject _uniqueAttackGO;
    private GameObject _zoneGO;
    private GameObject _fertilizerGO;
    private GameObject _forestGO;
    private GameObject _oreGO;

    private SpriteRenderer _selectorSR;
    private SpriteRenderer _spawnSR;
    private SpriteRenderer _wayUnitSR;
    private SpriteRenderer _simpleAttackSR;
    private SpriteRenderer _uniqueAttackSR;
    private SpriteRenderer _zoneSR;
    private SpriteRenderer _fertilizerSR;
    private SpriteRenderer _forestSR;
    private SpriteRenderer _oreSR;

    internal void Fill(GameObject supportVisionGO)
    {
        _selectorGO = supportVisionGO.transform.Find("Selector").gameObject;
        _spawnGO = supportVisionGO.transform.Find("Assignment").gameObject;
        _wayUnitGO = supportVisionGO.transform.Find("WayOfUnit").gameObject;
        _enemyGO = supportVisionGO.transform.Find("Enemy").gameObject;
        _uniqueAttackGO = supportVisionGO.transform.Find("UniqueAttack").gameObject;
        _zoneGO = supportVisionGO.transform.Find("Zone").gameObject;
        _fertilizerGO = supportVisionGO.transform.Find("Fertilizer").gameObject;
        _forestGO = supportVisionGO.transform.Find("Forest").gameObject;
        _oreGO = supportVisionGO.transform.Find("Ore").gameObject;

        _selectorSR = _selectorGO.GetComponent<SpriteRenderer>();
        _spawnSR = _spawnGO.GetComponent<SpriteRenderer>();
        _wayUnitSR = _wayUnitGO.GetComponent<SpriteRenderer>();
        _simpleAttackSR = _enemyGO.GetComponent<SpriteRenderer>();
        _uniqueAttackSR = _uniqueAttackGO.GetComponent<SpriteRenderer>();
        _zoneSR = _zoneGO.GetComponent<SpriteRenderer>();
        _fertilizerSR = _fertilizerGO.GetComponent<SpriteRenderer>();
        _forestSR = _forestGO.GetComponent<SpriteRenderer>();
        _oreSR = _oreGO.GetComponent<SpriteRenderer>();
    }


    internal void ActiveVision(bool isActive, SupportVisionTypes supportVisionType)
    {
        switch (supportVisionType)
        {
            case SupportVisionTypes.None:
                break;

            case SupportVisionTypes.Selector:
                _selectorSR.enabled = isActive;
                break;

            case SupportVisionTypes.Spawn:
                _spawnSR.enabled = isActive;
                break;

            case SupportVisionTypes.WayUnit:
                _wayUnitSR.enabled = isActive;
                break;

            case SupportVisionTypes.SimpleAttack:
                _simpleAttackSR.enabled = isActive;
                break;

            case SupportVisionTypes.UniqueAttack:
                _uniqueAttackSR.enabled = isActive;
                break;

            case SupportVisionTypes.Zone:
                _zoneSR.enabled = isActive;
                break;

            case SupportVisionTypes.Fertilize:
                _fertilizerSR.enabled = isActive;
                break;

            case SupportVisionTypes.Forest:
                _forestSR.enabled = isActive;
                break;

            case SupportVisionTypes.Ore:
                _oreSR.enabled = isActive;
                break;

            default:
                break;
        }
    }

    internal void SetScale(SupportVisionTypes supportVisionType, Vector3 vector)
    {
        switch (supportVisionType)
        {
            case SupportVisionTypes.None:
                break;
            case SupportVisionTypes.Selector:
                break;
            case SupportVisionTypes.Spawn:
                break;
            case SupportVisionTypes.WayUnit:
                break;
            case SupportVisionTypes.SimpleAttack:
                break;
            case SupportVisionTypes.UniqueAttack:
                break;
            case SupportVisionTypes.Zone:
                break;

            case SupportVisionTypes.Fertilize:
                _fertilizerGO.transform.localScale = vector;
                break;

            case SupportVisionTypes.Forest:
                _forestGO.transform.localScale = vector;
                break;

            case SupportVisionTypes.Ore:
                _oreGO.transform.localScale = vector;
                break;

            default:
                break;
        }
    }
}
