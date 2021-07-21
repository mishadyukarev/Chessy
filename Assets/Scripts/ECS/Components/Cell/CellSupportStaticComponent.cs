using System;
using UnityEngine;

internal struct CellSupportStaticComponent
{
    private SpriteRenderer _fertilizerSR;
    private SpriteRenderer _woodSR;
    private SpriteRenderer _oreSR;

    internal void Fill(GameObject supportStaticGO)
    {
        _fertilizerSR = supportStaticGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();
        _woodSR = supportStaticGO.transform.Find("Forest").GetComponent<SpriteRenderer>();
        _oreSR = supportStaticGO.transform.Find("Ore").GetComponent<SpriteRenderer>();
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
                _woodSR.color = color;
                break;

            case SupportStaticTypes.Ore:
                _oreSR.color = color;
                break;

            case SupportStaticTypes.Hp:
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
                _fertilizerSR.transform.localScale = vector;
                break;

            case SupportStaticTypes.Wood:
                _woodSR.transform.localScale = vector;
                break;

            case SupportStaticTypes.Ore:
                _oreSR.transform.localScale = vector;
                break;

            case SupportStaticTypes.Hp:
                break;

            default:
                break;
        }
    }
}
