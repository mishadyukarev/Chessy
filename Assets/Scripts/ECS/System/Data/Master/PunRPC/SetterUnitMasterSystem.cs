using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using System;

internal sealed class SetterUnitMasterSystem : SystemMasterReduction
{
    internal UnitTypes UnitType => _eMM.SettingUnitEnt_UnitTypeCom.UnitType;
    private int[] XyCell => _eMM.SettingUnitEnt_XyCellCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (!CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.Mountain, XyCell) && !CellUnitsDataContainer.HaveAnyUnit(XyCell)
            && InfoCellWorker.IsStartedCell(RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell))
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();


                case UnitTypes.King:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsDataContainer.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Pawn:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsDataContainer.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.PawnSword:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsDataContainer.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Rook:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsDataContainer.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.RookCrossbow:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Bishop:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsDataContainer.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.BishopCrossbow:
                    InfoUnitsDataContainer.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    InfoUnitsDataContainer.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataContainer.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InfoUnitsDataContainer.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;

                default:
                    throw new Exception();
            }

            PhotonPunRPC.SetUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender, true);
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.ClickToTable);
        }

        else
        {
            PhotonPunRPC.SetUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender, false);
        }
    }
}
