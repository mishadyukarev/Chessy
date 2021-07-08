using Assets.Scripts.Abstractions.Enums;
using Photon.Realtime;
using System;
using UnityEngine;
using static Assets.Scripts.Main;

internal struct CellUnitComponent
{
    private SpriteRenderer _kingSR;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _pawnSwordSR;
    private SpriteRenderer _rookSR;
    private SpriteRenderer _rookCrossbowSR;
    private SpriteRenderer _bishopSR;
    private SpriteRenderer _bishopCrossbowSR;
    private SpriteRenderer _standartColorSR;
    private SpriteRenderer _defendRelaxSR;
    private int _amountHealth;
    private int _amountSteps;
    private int _amountStepsInNone;
    private int _amountStepsInProtected;
    private int _amountStepsInRelaxed;

    internal int AmountHealth => _amountHealth;
    internal int AmountSteps => _amountSteps;

    internal bool HaveHealth => _amountHealth > 0;
    internal bool HaveMinAmountSteps => _amountSteps >= 1;

    internal void StartFill(GameObject unitParentGO)
    {
        _kingSR = unitParentGO.transform.Find("King").GetComponent<SpriteRenderer>();
        _pawnSR = unitParentGO.transform.Find("Pawn").GetComponent<SpriteRenderer>();
        _pawnSwordSR = unitParentGO.transform.Find("PawnSword").GetComponent<SpriteRenderer>();
        _rookSR = unitParentGO.transform.Find("Rook").GetComponent<SpriteRenderer>();
        _rookCrossbowSR = unitParentGO.transform.Find("RookCrossbow").GetComponent<SpriteRenderer>();
        _bishopSR = unitParentGO.transform.Find("Bishop").GetComponent<SpriteRenderer>();
        _bishopCrossbowSR = unitParentGO.transform.Find("BishopCrossbow").GetComponent<SpriteRenderer>();
        _standartColorSR = unitParentGO.transform.Find("Color").GetComponent<SpriteRenderer>();
        _defendRelaxSR = unitParentGO.transform.Find("DefendRelax").GetComponent<SpriteRenderer>();
    }

    internal int AmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType)
    {
        switch (protectRelaxType)
        {
            case ProtectRelaxTypes.None:
                return _amountStepsInNone;

            case ProtectRelaxTypes.Protected:
                return _amountStepsInProtected;

            case ProtectRelaxTypes.Relaxed:
                return _amountStepsInRelaxed;

            default:
                throw new Exception();
        }
    }
    internal void ResetAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType)
    {
        switch (protectRelaxType)
        {
            case ProtectRelaxTypes.None:
                _amountStepsInNone = default;
                break;

            case ProtectRelaxTypes.Protected:
                _amountStepsInProtected = default;
                break;

            case ProtectRelaxTypes.Relaxed:
                _amountStepsInRelaxed = default;
                break;

            default:
                throw new Exception();
        }
    }
    internal void AddAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int adding = 1)
    {
        switch (protectRelaxType)
        {
            case ProtectRelaxTypes.None:
                _amountStepsInNone += adding;
                break;

            case ProtectRelaxTypes.Protected:
                _amountStepsInProtected += adding;
                break;

            case ProtectRelaxTypes.Relaxed:
                _amountStepsInRelaxed += adding;
                break;

            default:
                throw new Exception();
        }
    }
    internal void TakeAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int taking = 1)
    {
        switch (protectRelaxType)
        {
            case ProtectRelaxTypes.None:
                _amountStepsInNone -= taking;
                break;

            case ProtectRelaxTypes.Protected:
                _amountStepsInProtected -= taking;
                break;

            case ProtectRelaxTypes.Relaxed:
                _amountStepsInRelaxed -= taking;
                break;

            default:
                throw new Exception();
        }
    }

    internal int SetAmountSteps(int value) => _amountSteps = value;
    internal int ResetAmountSteps() => _amountSteps = default;
    internal int AddAmountSteps(int adding = 1) => _amountSteps += adding;
    internal int TakeAmountSteps(int taking = 1) => _amountSteps -= taking;
    internal bool HaveMaxSteps(UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;

            case UnitTypes.Pawn:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;

            case UnitTypes.PawnSword:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN_SWORD;

            case UnitTypes.Rook:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;

            case UnitTypes.RookCrossbow:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

            case UnitTypes.Bishop:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;

            case UnitTypes.BishopCrossbow:
                return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;

            default:
                throw new Exception();
        }
    }
    internal void RefreshAmountSteps(UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.King:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING);
                break;

            case UnitTypes.Pawn:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN);
                break;

            case UnitTypes.PawnSword:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN_SWORD);
                break;

            case UnitTypes.Rook:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK);
                break;

            case UnitTypes.RookCrossbow:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW);
                break;

            case UnitTypes.Bishop:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP);
                break;

            case UnitTypes.BishopCrossbow:
                SetAmountSteps(Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW);
                break;

            default:
                break;
        }
    }

    internal int SetAmountHealth(int value) => _amountHealth = value;
    internal int ResetAmountHealth() => _amountHealth = default;
    internal int AddAmountHealth(int adding = 1) => _amountHealth += adding;
    internal int TakeAmountHealth(int taking = 1) => _amountHealth -= taking;

    internal void EnablePlayerSR(bool isActive, UnitTypes unitType, Player player)
    {
        EnableSR(isActive, unitType);

        if (player != default)
        {
            if (player.IsMasterClient)
            {
                _standartColorSR.color = Color.blue;
                _defendRelaxSR.color = Color.yellow;
            }
            else
            {
                _standartColorSR.color = Color.red;
                _defendRelaxSR.color = Color.yellow;
            }
        }
    }
    internal void EnableBotSR(bool isActive, UnitTypes unitType)
    {
        EnableSR(isActive, unitType);

        if (isActive)
        {
            _standartColorSR.color = Color.red;
        }
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
                _defendRelaxSR.enabled = isActive;
                break;

            case UnitTypes.Pawn:
                _pawnSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                _defendRelaxSR.enabled = isActive;
                break;

            case UnitTypes.PawnSword:
                _pawnSwordSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                _defendRelaxSR.enabled = isActive;
                break;

            case UnitTypes.Rook:
                _rookSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                _defendRelaxSR.enabled = isActive;
                break;

            case UnitTypes.RookCrossbow:
                _rookCrossbowSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                _defendRelaxSR.enabled = isActive;
                break;

            case UnitTypes.Bishop:
                _bishopSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                _defendRelaxSR.enabled = isActive;
                break;

            case UnitTypes.BishopCrossbow:
                _bishopCrossbowSR.enabled = isActive;
                _standartColorSR.enabled = isActive;
                _defendRelaxSR.enabled = isActive;
                break;

            default:
                break;
        }

    }

    internal void SetColorDefendRelaxSR(Color color) => _defendRelaxSR.color = color;
    internal void EnableDefendRelaxSR(bool isActive) => _defendRelaxSR.enabled = isActive;
    internal void EnableStandartColorSR(bool isActive) => _standartColorSR.enabled = isActive;

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

