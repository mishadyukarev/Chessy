using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Photon.Pun;

internal sealed class SetterUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
    internal UnitTypes UnitType => _eMM.SettingUnitEnt_UnitTypeCom.UnitType;
    private int[] XyCell => _eMM.SettingUnitEnt_XyCellCom.XyCell;

    public override void Run()
    {
        base.Run();

        bool isSetted = false;

        if (!CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.Mountain, XyCell) && !_eGM.CellUnitEnt_UnitTypeCom(XyCell).HaveAnyUnit)
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
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
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.PawnSword:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Rook:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.RookCrossbow:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    else isSetted = false;
                    break;


                case UnitTypes.Bishop:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType));
                        _eGM.UnitInfoEnt_UnitInventorCom.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                        isSetted = true;
                    }
                    break;


                case UnitTypes.BishopCrossbow:
                    if (_eGM.CellEnt_CellBaseCom(XyCell).IsStartedCell(InfoFrom.Sender.IsMasterClient))
                    {
                        CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
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
