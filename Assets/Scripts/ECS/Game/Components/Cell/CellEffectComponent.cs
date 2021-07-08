using System;
using UnityEngine;

internal struct CellEffectComponent
{
    private SpriteRenderer _fireSR;
    private bool _haveFire;
    private int _timeStepsFire;

    internal void StartFill(GameObject effectsGO)
    {
        _fireSR = effectsGO.transform.Find("Fire").GetComponent<SpriteRenderer>();
        _haveFire = default;
        _timeStepsFire = default;
    }

    internal bool HaveEffect(EffectTypes effectType)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                return _haveFire;

            default:
                throw new Exception();
        }
    }

    internal void SetEffect(EffectTypes effectType)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                _haveFire = true;
                _fireSR.enabled = true;
                break;

            default:
                break;
        }
    }

    internal void ResetEffect(EffectTypes effectType)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                _haveFire = false;
                _fireSR.enabled = false;
                _timeStepsFire = default;
                break;

            default:
                break;
        }
    }

    internal void SyncEffect(bool isActive, EffectTypes effectType)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                _timeStepsFire = default;
                break;

            case EffectTypes.Fire:
                _haveFire = isActive;
                _fireSR.enabled = isActive;
                break;

            default:
                break;
        }
    }

    internal int TimeStepsEffect(EffectTypes effectType)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                return _timeStepsFire;

            default:
                throw new Exception();
        }
    }

    internal void SetTimeStepsEffect(EffectTypes effectType, int value)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                _timeStepsFire = value;
                break;

            default:
                throw new Exception();
        }
    }

    internal void AddTimeStepsEffect(EffectTypes effectType, int adding = 1)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                _timeStepsFire += adding;
                break;

            default:
                throw new Exception();
        }
    }

    internal void TakeTimeStepsEffect(EffectTypes effectType, int adding = -1)
    {
        switch (effectType)
        {
            case EffectTypes.None:
                throw new Exception();

            case EffectTypes.Fire:
                _timeStepsFire += adding;
                break;

            default:
                throw new Exception();
        }
    }
}
