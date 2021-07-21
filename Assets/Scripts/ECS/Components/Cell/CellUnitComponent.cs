using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using System;

internal struct CellUnitComponent
{
    private int _amountHealth;
    private int _amountSteps;
    private int _amountStepsInNone;
    private int _amountStepsInProtected;
    private int _amountStepsInRelaxed;

    internal int AmountHealth => _amountHealth;
    internal int AmountSteps => _amountSteps;

    internal bool HaveAmountHealth => _amountHealth > 0;
    internal bool HaveMinAmountSteps => _amountSteps >= 1;

    internal void StartFill()
    {
        _amountHealth = default;
        _amountSteps = default;
        _amountStepsInNone = default;
        _amountStepsInProtected = default;
        _amountStepsInRelaxed = default;
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
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_KING;

            case UnitTypes.Pawn:
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_PAWN;

            case UnitTypes.PawnSword:
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_PAWN_SWORD;

            case UnitTypes.Rook:
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_ROOK;

            case UnitTypes.RookCrossbow:
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

            case UnitTypes.Bishop:
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_BISHOP;

            case UnitTypes.BishopCrossbow:
                return _amountSteps == UnitValues.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;

            default:
                throw new Exception();
        }
    }
    internal void RefreshAmountSteps(UnitTypes unitType)
    {
        switch (unitType)
        {
            case UnitTypes.King:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_KING);
                break;

            case UnitTypes.Pawn:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_PAWN);
                break;

            case UnitTypes.PawnSword:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_PAWN_SWORD);
                break;

            case UnitTypes.Rook:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_ROOK);
                break;

            case UnitTypes.RookCrossbow:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW);
                break;

            case UnitTypes.Bishop:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_BISHOP);
                break;

            case UnitTypes.BishopCrossbow:
                SetAmountSteps(UnitValues.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW);
                break;

            default:
                break;
        }
    }

    internal int SetAmountHealth(int value) => _amountHealth = value;
    internal int ResetAmountHealth() => _amountHealth = default;
    internal int AddAmountHealth(int adding = 1) => _amountHealth += adding;
    internal int TakeAmountHealth(int taking = 1) => _amountHealth -= taking;

    //internal void Flip(bool isActivated, UnitTypes unitType, XyTypes flipType)
    //{
    //    switch (flipType)
    //    {
    //        case XyTypes.X:
    //            switch (unitType)
    //            {
    //                case UnitTypes.None:
    //                    break;

    //                case UnitTypes.King:
    //                    _kingSR.flipX = isActivated;
    //                    break;

    //                case UnitTypes.Pawn:
    //                    _pawnSR.flipX = isActivated;
    //                    break;

    //                case UnitTypes.PawnSword:
    //                    _pawnSwordSR.flipX = isActivated;
    //                    break;

    //                case UnitTypes.Rook:
    //                    _rookSR.flipX = isActivated;
    //                    break;

    //                case UnitTypes.RookCrossbow:
    //                    _rookCrossbowSR.flipX = isActivated;
    //                    break;

    //                case UnitTypes.Bishop:
    //                    _bishopSR.flipX = isActivated;
    //                    break;

    //                case UnitTypes.BishopCrossbow:
    //                    _bishopCrossbowSR.flipX = isActivated;
    //                    break;

    //                default:
    //                    break;
    //            }
    //            break;

    //        case XyTypes.Y:
    //            switch (unitType)
    //            {
    //                case UnitTypes.None:
    //                    break;

    //                case UnitTypes.King:
    //                    _kingSR.flipY = isActivated;
    //                    break;

    //                case UnitTypes.Pawn:
    //                    _pawnSR.flipY = isActivated;
    //                    break;

    //                case UnitTypes.PawnSword:
    //                    _pawnSwordSR.flipY = isActivated;
    //                    break;

    //                case UnitTypes.Rook:
    //                    _rookSR.flipY = isActivated;
    //                    break;

    //                case UnitTypes.RookCrossbow:
    //                    _rookCrossbowSR.flipY = isActivated;
    //                    break;

    //                case UnitTypes.Bishop:
    //                    _bishopSR.flipY = isActivated;
    //                    break;

    //                case UnitTypes.BishopCrossbow:
    //                    _bishopCrossbowSR.flipY = isActivated;
    //                    break;

    //                default:
    //                    break;
    //            }
    //            break;

    //        default:
    //            break;
    //    }
    //}
    //internal void SetRotation(UnitTypes unitType, float x, float y, float z)
    //{
    //    switch (unitType)
    //    {
    //        case UnitTypes.None:
    //            break;

    //        case UnitTypes.King:
    //            _kingSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        case UnitTypes.Pawn:
    //            _pawnSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        case UnitTypes.PawnSword:
    //            _pawnSwordSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        case UnitTypes.Rook:
    //            _rookSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        case UnitTypes.RookCrossbow:
    //            _rookCrossbowSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        case UnitTypes.Bishop:
    //            _bishopSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        case UnitTypes.BishopCrossbow:
    //            _bishopCrossbowSR.transform.rotation = Quaternion.Euler(x, y, z);
    //            break;

    //        default:
    //            break;
    //    }
    //}
}

