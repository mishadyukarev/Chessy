using UnityEngine;

internal struct CellEffectComponent
{
    private bool _haveFire;
    private GameObject _fireGO;

    internal int TimeFire;
    internal bool HaveFire => _haveFire;


    internal void Fill(GameObject effectsGO)
    {
        _fireGO = effectsGO.transform.Find("Fire").gameObject;
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
