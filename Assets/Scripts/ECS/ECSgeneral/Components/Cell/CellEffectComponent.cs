using UnityEngine;

internal struct CellEffectComponent
{
    private bool _haveFire;
    private GameObject _fireGO;

    internal int TimeFire;
    internal bool HaveFire => _haveFire;



    internal CellEffectComponent(ObjectPoolGame gameObjectPool, int x, int y)
    {
        _haveFire = default;
        _fireGO = gameObjectPool.CellEffectFireGOs[x, y];
        TimeFire = default;
    }

    internal void SetResetEffect(bool isActive, EffectTypes effectType)
    {
        switch (effectType)
        {
            case EffectTypes.Fire:
                _haveFire = isActive;
                _fireGO.SetActive(isActive);
                if (!isActive) TimeFire = 0;
                break;

            default:
                break;
        }
    }
}
