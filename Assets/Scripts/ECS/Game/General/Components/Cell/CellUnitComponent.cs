using Assets.Scripts.Abstractions.Enums;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

internal struct CellUnitComponent
{
    private GameObject _pawnGO;
    private GameObject _kingGO;
    private GameObject _rookGO;
    private GameObject _bishopGO;

    private float _standartX;
    private float _standartY;
    private float _standartZ;

    private SpriteRenderer _kingSR;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _pawnSwordSR;
    private SpriteRenderer _rookSR;
    private SpriteRenderer _bishopSR;
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

    internal void Fill(GameObject unitParentGO)
    {
        IsActivatedUnitDict = new Dictionary<bool, bool>();
        IsActivatedUnitDict.Add(true, default);
        IsActivatedUnitDict.Add(false, default);

        _kingGO = unitParentGO.transform.Find("King").gameObject;
        _pawnGO = unitParentGO.transform.Find("Pawn").gameObject;
        _rookGO = unitParentGO.transform.Find("Rook").gameObject;
        _bishopGO = unitParentGO.transform.Find("Bishop").gameObject;

        _kingSR = _kingGO.GetComponent<SpriteRenderer>();
        _pawnSR = _pawnGO.GetComponent<SpriteRenderer>();
        _pawnSwordSR = unitParentGO.transform.Find("PawnSword").GetComponent<SpriteRenderer>();
        _rookSR = _rookGO.GetComponent<SpriteRenderer>();
        _bishopSR = _bishopGO.GetComponent<SpriteRenderer>();
        _standartColorSR = unitParentGO.transform.Find("Color").GetComponent<SpriteRenderer>();

        _standartX = unitParentGO.transform.parent.transform.rotation.eulerAngles.x;
        _standartY = unitParentGO.transform.parent.transform.rotation.eulerAngles.y;
        _standartZ = unitParentGO.transform.parent.transform.rotation.eulerAngles.z;
    }

    internal void EnableSR(bool isActive, UnitTypes unitType, Player player = default)
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

            case UnitTypes.Bishop:
                _bishopSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            default:
                break;
        }

        if (player != default && _standartColorSR != default)
        {
            if (player.IsMasterClient) _standartColorSR.color = Color.blue;
            else _standartColorSR.color = Color.red;
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

                    case UnitTypes.Bishop:
                        _bishopSR.flipX = isActivated;
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

                    case UnitTypes.Bishop:
                        _bishopSR.flipY = isActivated;
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

            case UnitTypes.Bishop:
                _bishopSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            default:
                break;
        }
    }
}

