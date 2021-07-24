using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using System;

internal sealed class SetterUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
    internal UnitTypes UnitType => _eMM.SettingUnitEnt_UnitTypeCom.UnitType;
    private int[] XyCell => _eMM.SettingUnitEnt_XyCellCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (!CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.Mountain, XyCell) && !CellUnitWorker.HaveAnyUnit(XyCell)
            && InfoCellWorker.IsStartedCell(InfoFrom.Sender.IsMasterClient, XyCell))
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();


                case UnitTypes.King:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsWorker.AddAmountUnitInGame(UnitType, InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    InfoUnitsWorker.SetSettedKing(InfoFrom.Sender.IsMasterClient, true);
                    break;


                case UnitTypes.Pawn:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsWorker.AddAmountUnitInGame(UnitType, InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.PawnSword:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsWorker.AddAmountUnitInGame(UnitType, InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Rook:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsWorker.AddAmountUnitInGame(UnitType, InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.RookCrossbow:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Bishop:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsWorker.AddAmountUnitInGame(UnitType, InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.BishopCrossbow:
                    InfoUnitsWorker.AddUnitInStandartCondition(ConditionTypes.None, UnitType, InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsWorker.AddAmountUnitInGame(UnitType, InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitWorker.SetNewPlayerUnit(UnitType, InfoFrom.Sender, XyCell);
                    CellUnitWorker.SetAmountHealth(CellUnitWorker.MaxAmountHealth(UnitType), XyCell);
                    InfoUnitsWorker.TakeUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                    break;

                default:
                    throw new Exception();
            }

            PhotonPunRPC.SetUnitToGeneral(InfoFrom.Sender, true);
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.ClickToTable);
        }

        else
        {
            PhotonPunRPC.SetUnitToGeneral(InfoFrom.Sender, false);
        }
    }
}
