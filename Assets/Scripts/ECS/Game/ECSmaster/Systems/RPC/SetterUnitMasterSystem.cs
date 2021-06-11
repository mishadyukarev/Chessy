using Photon.Pun;

internal sealed class SetterUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
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

                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(Info.Sender.IsMasterClient))
                    {
                        _cM.CellUnitWorker.SetUnit(UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInventorEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;


                    _eGM.UnitInventorEnt_UnitInventorCom.SetSettedKing(Info.Sender.IsMasterClient, isSetted);

                    break;


                case UnitTypes.Pawn:

                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(Info.Sender.IsMasterClient))
                    {
                        _cM.CellUnitWorker.SetUnit(UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInventorEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;


                    break;


                case UnitTypes.Rook:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(true))
                    {
                        _cM.CellUnitWorker.SetUnit(UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInventorEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Bishop:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(true))
                    {
                        _cM.CellUnitWorker.SetUnit(UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _cM.CellUnitWorker.MaxAmountHealth(XyCell);
                        _eGM.UnitInventorEnt_UnitInventorCom.TakeAmountUnits(UnitType, Info.Sender.IsMasterClient);
                        isSetted = true;
                    }

                    break;


                default:
                    break;
            }
        }
        _photonPunRPC.SetUniToGeneral(Info.Sender, isSetted);
    }
}
