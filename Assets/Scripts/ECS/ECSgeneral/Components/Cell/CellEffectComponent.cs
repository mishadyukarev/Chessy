using UnityEngine;

internal struct CellEffectComponent
{
    private EntitiesGeneralManager _eGM;
    private int[] _xy;
    private bool _isActiveFire;
    private GameObject _fireGO;

    internal int TimeFire;
    internal bool IsFired;
    internal bool HaveFire => _isActiveFire;



    internal CellEffectComponent(EntitiesGeneralManager eGM,GameObjectPool gameObjectPool, params int[] xy)
    {
        _eGM = eGM;
        _xy = xy;

        _isActiveFire = default;
        _fireGO = gameObjectPool.CellEffectFireGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        TimeFire = default;
        IsFired = default;
    }

    internal void SetEffect(bool isActive, EffectTypes effectType)
    {
        _fireGO.SetActive(false);

        switch (effectType)
        {
            case EffectTypes.Fire:
                _isActiveFire = isActive;
                _fireGO.SetActive(isActive);
                TimeFire = 0;
                IsFired = !isActive;
                break;

            default:
                break;
        }
    }
}
