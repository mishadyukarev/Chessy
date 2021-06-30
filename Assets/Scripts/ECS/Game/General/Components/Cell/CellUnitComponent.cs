using Assets.Scripts.Abstractions.Enums;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

internal struct CellUnitComponent
{
    private float _standartX;
    private float _standartY;
    private float _standartZ;

    private SpriteRenderer _kingSR;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _pawnSwordSR;
    private SpriteRenderer _rookSR;
    private SpriteRenderer _rookCrossbowSR;
    private SpriteRenderer _bishopSR;
    private SpriteRenderer _bishopCrossbowSR;
    private SpriteRenderer _standartColorSR;

    internal Dictionary<bool, bool> IsActivatedUnitDict;
    internal bool IsProtected;
    internal bool IsRelaxed;
    internal int AmountHealth;
    internal int AmountSteps;

    internal bool HaveHealth => AmountHealth > 0;
    internal bool HaveMinAmountSteps => AmountSteps >= 1;

    internal float StandartX => _standartX;
    internal float StandartY => _standartY;
    internal float StandartZ => _standartZ;

    internal void StartFill(GameObject unitParentGO)
    {
        IsActivatedUnitDict = new Dictionary<bool, bool>();
        IsActivatedUnitDict.Add(true, default);
        IsActivatedUnitDict.Add(false, default);

        _kingSR = unitParentGO.transform.Find("King").GetComponent<SpriteRenderer>();
        _pawnSR = unitParentGO.transform.Find("Pawn").GetComponent<SpriteRenderer>();
        _pawnSwordSR = unitParentGO.transform.Find("PawnSword").GetComponent<SpriteRenderer>();
        _rookSR = unitParentGO.transform.Find("Rook").GetComponent<SpriteRenderer>();
        _rookCrossbowSR = unitParentGO.transform.Find("RookCrossbow").GetComponent<SpriteRenderer>();
        _bishopSR = unitParentGO.transform.Find("Bishop").GetComponent<SpriteRenderer>();
        _bishopCrossbowSR = unitParentGO.transform.Find("BishopCrossbow").GetComponent<SpriteRenderer>();
        _standartColorSR = unitParentGO.transform.Find("Color").GetComponent<SpriteRenderer>();

        _standartX = unitParentGO.transform.parent.transform.rotation.eulerAngles.x;
        _standartY = unitParentGO.transform.parent.transform.rotation.eulerAngles.y;
        _standartZ = unitParentGO.transform.parent.transform.rotation.eulerAngles.z;
    }

    internal void EnablePlayerSR(bool isActive, UnitTypes unitType, Player player)
    {
        EnableSR(isActive, unitType);

        if (player != default)
        {
            if (player.IsMasterClient) _standartColorSR.color = Color.blue;
            else _standartColorSR.color = Color.red;
        }
    }

    internal void EnableBotSR(bool isActive, UnitTypes unitType)
    {
        EnableSR(isActive, unitType);

        if (isActive) _standartColorSR.color = Color.red;
    }

    internal void EnableSR(bool isActive, UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                _kingSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.Pawn:
                _pawnSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.PawnSword:
                _pawnSwordSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.Rook:
                _rookSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.RookCrossbow:
                _rookCrossbowSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.Bishop:
                _bishopSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.BishopCrossbow:
                _bishopCrossbowSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            default:
                break;
        }

    }

    internal void Flip(bool isActivated, UnitTypes unitType, XyTypes flipType)
    {
        switch (flipType)
        {
            case XyTypes.X:
                switch (unitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        _kingSR.flipX = isActivated;
                        break;

                    case UnitTypes.Pawn:
                        _pawnSR.flipX = isActivated;
                        break;

                    case UnitTypes.PawnSword:
                        _pawnSwordSR.flipX = isActivated;
                        break;

                    case UnitTypes.Rook:
                        _rookSR.flipX = isActivated;
                        break;

                    case UnitTypes.RookCrossbow:
                        _rookCrossbowSR.flipX = isActivated;
                        break;

                    case UnitTypes.Bishop:
                        _bishopSR.flipX = isActivated;
                        break;

                    case UnitTypes.BishopCrossbow:
                        _bishopCrossbowSR.flipX = isActivated;
                        break;

                    default:
                        break;
                }
                break;

            case XyTypes.Y:
                switch (unitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        _kingSR.flipY = isActivated;
                        break;

                    case UnitTypes.Pawn:
                        _pawnSR.flipY = isActivated;
                        break;

                    case UnitTypes.PawnSword:
                        _pawnSwordSR.flipY = isActivated;
                        break;

                    case UnitTypes.Rook:
                        _rookSR.flipY = isActivated;
                        break;

                    case UnitTypes.RookCrossbow:
                        _rookCrossbowSR.flipY = isActivated;
                        break;

                    case UnitTypes.Bishop:
                        _bishopSR.flipY = isActivated;
                        break;

                    case UnitTypes.BishopCrossbow:
                        _bishopCrossbowSR.flipY = isActivated;
                        break;

                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    internal void SetRotation(UnitTypes unitType, float x, float y, float z)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                _kingSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.Pawn:
                _pawnSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.PawnSword:
                _pawnSwordSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.Rook:
                _rookSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.RookCrossbow:
                _rookCrossbowSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.Bishop:
                _bishopSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.BishopCrossbow:
                _bishopCrossbowSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            default:
                break;
        }
    }
}

