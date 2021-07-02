using System;
using UnityEngine;

internal struct CellSupportStaticComponent
{
    private GameObject _fertilizerGO;
    private GameObject _woodGO;
    private GameObject _oreGO;

    private SpriteRenderer _fertilizerSR;
    private SpriteRenderer _woodSR;
    private SpriteRenderer _oreSR;
    private SpriteRenderer _hpSR;


    internal void Fill(GameObject supportStaticGO)
    {
        _fertilizerGO = supportStaticGO.transform.Find("Fertilizer").gameObject;
        _woodGO = supportStaticGO.transform.Find("Forest").gameObject;
        _oreGO = supportStaticGO.transform.Find("Ore").gameObject;

        _fertilizerSR = _fertilizerGO.GetComponent<SpriteRenderer>();
        _woodSR = _woodGO.GetComponent<SpriteRenderer>();
        _oreSR = _oreGO.GetComponent<SpriteRenderer>();
        _hpSR = supportStaticGO.transform.Find("Hp").GetComponent<SpriteRenderer>();
    }

    internal void ActiveVision(bool isActive, SupportStaticTypes supportStaticType)
    {
        switch (supportStaticType)
        {
            case SupportStaticTypes.Fertilizer:
                _fertilizerSR.enabled = isActive;
                break;

            case SupportStaticTypes.Wood:
                _woodSR.enabled = isActive;
                break;

            case SupportStaticTypes.Ore:
                _oreSR.enabled = isActive;
                break;

            case SupportStaticTypes.Hp:
                _hpSR.enabled = isActive;
                break;

            default:
                throw new Exception();
        }
    }

    internal void SetColor(SupportStaticTypes supportStaticType, Color color)
    {
        switch (supportStaticType)
        {
            case SupportStaticTypes.Fertilizer:
                _fertilizerSR.color = color;
                break;

            case SupportStaticTypes.Wood:
                _woodSR.color  = color;
                break;

            case SupportStaticTypes.Ore:
                _oreSR.color= color;
                break;

            case SupportStaticTypes.Hp:
                _hpSR.color = color;
                break;

            default:
                throw new Exception();
        }
    }

    internal void SetScale(SupportStaticTypes supportStaticType, Vector3 vector)
    {
        switch (supportStaticType)
        {
            case SupportStaticTypes.Fertilizer:
                _fertilizerGO.transform.localScale = vector;
                break;

            case SupportStaticTypes.Wood:
                _woodGO.transform.localScale = vector;
                break;

            case SupportStaticTypes.Ore:
                _oreGO.transform.localScale = vector;
                break;

            case SupportStaticTypes.Hp:
                _hpSR.transform.localScale = vector;
                break;

            default:
                break;
        }
    }
}
