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

        if (!_eGM.CellEnvironmentComponent(XyCell).HaveMountain && !_eGM.CellUnitComponent(XyCell).HaveUnit)
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.King, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellComponent(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.King, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    _eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[Info.Sender.IsMasterClient] = isSetted;

                    break;


                case UnitTypes.Pawn:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    else
                    {
                        if (_eGM.CellComponent(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Rook:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.Rook, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountRookDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellComponent(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.Rook, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountRookDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Bishop:

                    if (Info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(XyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.Bishop, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellComponent(XyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(XyCell).SetUnit(UnitTypes.Bishop, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, Info.Sender);
                            _eGM.CellUnitComponent(XyCell).AmountHealth = _eGM.CellUnitComponent(XyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
