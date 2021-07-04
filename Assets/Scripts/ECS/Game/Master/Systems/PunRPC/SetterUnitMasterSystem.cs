using Assets.Scripts;
using Photon.Pun;

internal sealed class SetterUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    public override void Run()
    {
        base.Run();

        bool isSetted = false;

        if (!_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveMountain && !_eGM.CellUnitEnt_UnitTypeCom(XyCell).HaveUnit)
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;

                    _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(Info.Sender.IsMasterClient, isSetted);
                    break;


                case UnitTypes.Pawn:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.PawnSword:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitType, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN_SWORD, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Rook:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.RookCrossbow:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitType, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Bishop:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    break;


                case UnitTypes.BishopCrossbow:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(Info.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetPlayerUnit(UnitType, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;

                default:
                    break;
            }
        }
        _photonPunRPC.SetUniToGeneral(Info.Sender, isSetted);
    }
}
