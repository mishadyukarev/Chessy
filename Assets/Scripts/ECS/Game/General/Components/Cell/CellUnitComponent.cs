using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

internal struct CellUnitComponent
{
    private GameObject _pawnGO;
    private GameObject _kingGO;
    private GameObject _rookGO;
    private GameObject _bishopGO;

    private SpriteRenderer _kingSR;
    private SpriteRenderer _kingColorSR;
    private SpriteRenderer _pawnParentSR;
    private SpriteRenderer _pawnColorSR;
    private SpriteRenderer _rookParentSR;
    private SpriteRenderer _rookColorSR;
    private SpriteRenderer _bishopSR;
    private SpriteRenderer _bishopColorSR;

    internal Dictionary<bool, bool> IsActivatedUnitDict;
    internal bool IsProtected;
    internal bool IsRelaxed;
    internal int AmountHealth;
    internal int AmountSteps;
    internal bool HaveHealth => AmountHealth > 0;
    internal bool HaveMinAmountSteps => AmountSteps >= 1;

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
        _kingColorSR = _kingGO.transform.Find("GG").GetComponent<SpriteRenderer>();
        _pawnParentSR = _pawnGO.GetComponent<SpriteRenderer>();
        _pawnColorSR = _pawnParentSR.transform.Find("GG").GetComponent<SpriteRenderer>();
        _rookParentSR = _rookGO.GetComponent<SpriteRenderer>();
        _rookColorSR = _rookGO.transform.Find("GG").GetComponent<SpriteRenderer>();
        _bishopSR = _bishopGO.GetComponent<SpriteRenderer>();
        _bishopColorSR = _bishopSR.transform.Find("GG").GetComponent<SpriteRenderer>();
    }

    internal void EnableSR(bool isActive, UnitTypes unitType, Player player = default)
    {
        SpriteRenderer sR;

        switch (unitType)
        {
            case UnitTypes.None:
                sR = default;
                break;

            case UnitTypes.King:
                sR = _kingColorSR;
                sR.enabled = isActive;
                _kingSR.enabled = isActive;
                break;

            case UnitTypes.Pawn:
                sR = _pawnColorSR;
                sR.enabled = isActive;
                _pawnParentSR.enabled = isActive;
                break;

            case UnitTypes.Rook:
                sR = _rookColorSR;
                sR.enabled = isActive;
                _rookParentSR.enabled = isActive;
                break;

            case UnitTypes.Bishop:
                sR = _bishopColorSR;
                sR.enabled = isActive;
                _bishopSR.enabled = isActive;
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
}

