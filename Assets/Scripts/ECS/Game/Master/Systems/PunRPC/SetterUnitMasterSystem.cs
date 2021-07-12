using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

internal sealed class SetterUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    public override void Run()
    {
        base.Run();

        bool isSetted = false;

        if (!_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveEnvironment(EnvironmentTypes.Mountain) && !_eGM.CellUnitEnt_UnitTypeCom(XyCell).HaveUnit)
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;

                    _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(InfoFrom.Sender.IsMasterClient, isSetted);
                    break;


                case UnitTypes.Pawn:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.PawnSword:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitType, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN_SWORD, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Rook:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.RookCrossbow:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitType, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Bishop:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    break;


                case UnitTypes.BishopCrossbow:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(true, UnitType, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW, ProtectRelaxTypes.None, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;

                default:
                    break;
            }
        }
        if (isSetted) _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
        _photonPunRPC.SetUnitToGeneral(InfoFrom.Sender, isSetted);
    }
}
