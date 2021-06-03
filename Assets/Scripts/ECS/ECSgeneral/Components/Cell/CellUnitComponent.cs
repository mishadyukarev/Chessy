using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

internal struct CellUnitComponent : IDisposable
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


    internal CellUnitComponent(List<IDisposable> disposables)
    {
        IsActivatedUnitDict = new Dictionary<bool, bool>();
        AmountSteps = default;
        AmountHealth = default;
        IsProtected = default;
        IsRelaxed = default;

        _pawnGO = default;
        _kingGO = default;
        _rookGO = default;
        _bishopGO = default;

        _kingSR = default;
        _pawnSR = default;
        _rookSR = default;
        _bishopSR = default;

        disposables.Add(this);
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

    internal void Fill(ObjectPoolGame gameObjectPool, int x, int y)
    {
        _pawnGO = gameObjectPool.CellUnitPawnGOs[x, y];
        _kingGO = gameObjectPool.CellUnitKingGOs[x, y];
        _rookGO = gameObjectPool.CellUnitRookGOs[x, y];
        _bishopGO = gameObjectPool.CellUnitBishopGOs[x, y];

        _kingSR = gameObjectPool.CellUnitKingSRs[x, y];
        _pawnSR = gameObjectPool.CellUnitPawnSRs[x, y];
        _rookSR = gameObjectPool.CellUnitRookSRs[x, y];
        _bishopSR = gameObjectPool.CellUnitBishopSRs[x, y];

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

