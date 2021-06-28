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
    private SpriteRenderer _pawnParentSR;
    private SpriteRenderer _rookParentSR;
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
        _pawnParentSR = _pawnGO.GetComponent<SpriteRenderer>();
        _rookParentSR = _rookGO.GetComponent<SpriteRenderer>();
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
                _pawnParentSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                break;

            case UnitTypes.Rook:
                _rookParentSR.enabled = isActive;
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
                        _pawnParentSR.flipX = isActivated;
                        break;

                    case UnitTypes.Rook:
                        _rookParentSR.flipX = isActivated;
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
                        _pawnParentSR.flipY = isActivated;
                        break;

                    case UnitTypes.Rook:
                        _rookParentSR.flipY = isActivated;
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
                _pawnParentSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.Rook:
                _rookParentSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            case UnitTypes.Bishop:
                _bishopSR.transform.rotation = Quaternion.Euler(x, y, z);
                break;

            default:
                break;
        }
    }

    internal float GetX(UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new System.Exception();

            case UnitTypes.King:
                return _kingSR.transform.rotation.x;

            case UnitTypes.Pawn:
                return _pawnParentSR.transform.rotation.x;

            case UnitTypes.Rook:
                return _rookParentSR.transform.rotation.x;

            case UnitTypes.Bishop:
                return _bishopSR.transform.rotation.x;

            default:
                throw new System.Exception();
        }
    }

    internal float GetY(UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new System.Exception();

            case UnitTypes.King:
                return _kingSR.transform.rotation.y;

            case UnitTypes.Pawn:
                return _pawnParentSR.transform.rotation.y;

            case UnitTypes.Rook:
                return _rookParentSR.transform.rotation.y;

            case UnitTypes.Bishop:
                return _bishopSR.transform.rotation.y;

            default:
                throw new System.Exception();
        }
    }

    internal float GetZ(UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new System.Exception();

            case UnitTypes.King:
                return _kingSR.transform.rotation.z;

            case UnitTypes.Pawn:
                return _pawnParentSR.transform.rotation.z;

            case UnitTypes.Rook:
                return _rookParentSR.transform.rotation.z;

            case UnitTypes.Bishop:
                return _bishopSR.transform.rotation.z;

            default:
                throw new System.Exception();
        }
    }
}

