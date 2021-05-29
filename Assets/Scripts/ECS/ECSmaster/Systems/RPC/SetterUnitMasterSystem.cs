using Leopotam.Ecs;
using Photon.Pun;

internal class SetterUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    internal SetterUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        bool isSetted = false;

        if (!_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveMountain && !_eGM.CellEnt_CellUnitCom(XyCell).HaveUnit)
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(true))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountKingDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(false))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountKingDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[Info.Sender.IsMasterClient] = isSetted;

                    break;


                case UnitTypes.Pawn:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(true))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    else
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(false))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Rook:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(true))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(false))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Bishop:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(true))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellBaseCom(XyCell).IsStarted(false))
                        {
                            _eGM.CellEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender);
                            _eGM.CellEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                default:
                    break;
            }
        }
        _photonPunRPC.SetUniToGeneral(Info.Sender, isSetted);
    }
}
