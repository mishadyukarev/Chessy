using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using System;

internal sealed class SetterUnitMasterSystem : SystemMasterReduction
{
    internal UnitTypes UnitType => _eMM.SettingUnitEnt_UnitTypeCom.UnitType;
    private int[] XyCell => _eMM.SettingUnitEnt_XyCellCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, XyCell) && !CellUnitsDataWorker.HaveAnyUnit(XyCell)
            && InfoCellWorker.IsStartedCell(RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell))
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();


                case UnitTypes.King:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsContainer.AddAmountUnitInGame(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Pawn:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsContainer.AddAmountUnitInGame(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.PawnSword:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsContainer.AddAmountUnitInGame(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Rook:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsContainer.AddAmountUnitInGame(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.RookCrossbow:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Bishop:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsContainer.AddAmountUnitInGame(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.BishopCrossbow:
                    InfoUnitsContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsContainer.AddAmountUnitInGame(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataWorker.SetNewPlayerUnit(UnitType, RpcWorker.InfoFrom.Sender, XyCell);
                    InfoUnitsContainer.TakeUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                    break;

                default:
                    throw new Exception();
            }

            PhotonPunRPC.SetUnitToGeneral(RpcWorker.InfoFrom.Sender, true);
            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.ClickToTable);
        }

        else
        {
            PhotonPunRPC.SetUnitToGeneral(RpcWorker.InfoFrom.Sender, false);
        }
    }
}
