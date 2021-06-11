using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

internal struct CellUnitComponent
{
    private GameObject _pawnGO;
    private GameObject _kingGO;
    private GameObject _rookGO;
    private GameObject _bishopGO;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _kingSR;
    private SpriteRenderer _rookSR;
    private SpriteRenderer _bishopSR;

    internal Dictionary<bool, bool> IsActivatedUnitDict;
    internal bool IsProtected;
    internal bool IsRelaxed;
    internal int AmountHealth;
    internal int AmountSteps;
    internal bool HaveHealth => AmountHealth > 0;
    internal bool HaveMinAmountSteps => AmountSteps >= 1;


    internal void EnableSR(bool isActive, UnitTypes unitType, Player player = default)
    {
        SpriteRenderer sR;

        switch (unitType)
        {
            case UnitTypes.None:
                sR = default;
                break;

            case UnitTypes.King:
                sR = _kingSR;
                sR.enabled = isActive;
                break;

            case UnitTypes.Pawn:
                sR = _pawnSR;
                sR.enabled = isActive;
                break;

            case UnitTypes.Rook:
                sR = _rookSR;
                sR.enabled = isActive;
                break;

            case UnitTypes.Bishop:
                sR = _bishopSR;
                sR.enabled = isActive;
                break;

            default:
                sR = default;
                break;
        }

        if (player != default && sR != default)
        {
            if (player.IsMasterClient) sR.color = Color.blue;
            else sR.color = Color.red;
        }
    }

    internal void Fill(GameObject environmentGO)
    {
        IsActivatedUnitDict = new Dictionary<bool, bool>();
        IsActivatedUnitDict.Add(true, default);
        IsActivatedUnitDict.Add(false, default);

        _kingGO = environmentGO.transform.Find("King").gameObject;
        _pawnGO = environmentGO.transform.Find("Pawn").gameObject;
        _rookGO = environmentGO.transform.Find("Rook").gameObject;
        _bishopGO = environmentGO.transform.Find("Bishop").gameObject;

        _kingSR = _kingGO.GetComponent<SpriteRenderer>();
        _pawnSR = _pawnGO.GetComponent<SpriteRenderer>();
        _rookSR = _rookGO.GetComponent<SpriteRenderer>();
        _bishopSR = _bishopGO.GetComponent<SpriteRenderer>();
    }
    public void Dispose()
    {
        _pawnGO = default;
        _kingGO = default;
        _rookGO = default;
        _bishopGO = default;

        _pawnSR = default;
        _kingSR = default;
        _rookSR = default;
        _bishopGO = default;

        IsActivatedUnitDict[true] = default;
        IsActivatedUnitDict[false] = default;

        IsProtected = default;
        IsRelaxed = default;
        AmountHealth = default;
        AmountSteps = default;
    }
}

