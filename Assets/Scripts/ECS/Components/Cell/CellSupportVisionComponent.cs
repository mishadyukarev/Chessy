using UnityEngine;

internal struct CellSupportVisionComponent
{
    private SpriteRenderer _selectorSR;
    private SpriteRenderer _spawnSR;
    private SpriteRenderer _wayUnitSR;
    private SpriteRenderer _simpleAttackSR;
    private SpriteRenderer _uniqueAttackSR;
    private SpriteRenderer _zoneSR;
    private SpriteRenderer _upgradeSR;

    internal void Fill(GameObject supportVisionGO)
    {
        _selectorSR = supportVisionGO.transform.Find("Selector").GetComponent<SpriteRenderer>();
        _spawnSR = supportVisionGO.transform.Find("Assignment").GetComponent<SpriteRenderer>();
        _wayUnitSR = supportVisionGO.transform.Find("WayOfUnit").GetComponent<SpriteRenderer>();
        _simpleAttackSR = supportVisionGO.transform.Find("Enemy").GetComponent<SpriteRenderer>();
        _uniqueAttackSR = supportVisionGO.transform.Find("UniqueAttack").GetComponent<SpriteRenderer>();
        _zoneSR = supportVisionGO.transform.Find("Zone").GetComponent<SpriteRenderer>();
        _upgradeSR = supportVisionGO.transform.Find("Upgrade").GetComponent<SpriteRenderer>();
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

            case SupportVisionTypes.Upgrade:
                _upgradeSR.enabled = isActive;
                break;

            default:
                break;
        }
    }
}
