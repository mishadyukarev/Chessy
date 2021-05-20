using Leopotam.Ecs;
using Photon.Pun;

internal class SetterUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;
    internal UnitTypes UnitType => _eMM.MasterRPCEntUnitTypeCom.UnitType;
    private int[] XyCell => _eMM.MasterRPCEntXyCellCom.XyCell;

    internal SetterUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        bool isSetted = false;

        if (!_eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveMountain && !_eGM.CellUnitEnt_UnitTypeCom(XyCell).HaveUnit)
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.King, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountKingDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.King, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountKingDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[Info.Sender.IsMasterClient] = isSetted;

                    break;


                case UnitTypes.Pawn:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    else
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Rook:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Rook, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Rook, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Bishop:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Bishop, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellEnt_CellCom(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).SetUnit(UnitTypes.Bishop, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountHealth = _eGM.CellUnitEnt_CellUnitCom(XyCell).MaxAmountHealth;
                            _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
