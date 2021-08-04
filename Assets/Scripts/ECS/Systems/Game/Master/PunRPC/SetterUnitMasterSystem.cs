using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using System;

internal sealed class SetterUnitMasterSystem : SystemMasterReduction
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    internal UnitTypes UnitType => _eMM.SettingUnitEnt_UnitTypeCom.UnitType;
    private int[] XyCell => _eMM.SettingUnitEnt_XyCellCom.XyCell;

    public override void Run()
    {
        base.Run();

        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, XyCell) && !CellUnitsDataSystem.HaveAnyUnit(XyCell)
            && InitSystem.XyStartCellsCom.IsStartedCell(RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell))
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();


                case UnitTypes.King:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    xyUnitsCom.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Pawn:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    xyUnitsCom.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.PawnSword:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    xyUnitsCom.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Rook:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    xyUnitsCom.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.RookCrossbow:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.Bishop:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    xyUnitsCom.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                    break;


                case UnitTypes.BishopCrossbow:
                    InitSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);

                    xyUnitsCom.AddAmountUnitInGame(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, XyCell);
                    CellUnitsDataSystem.SetNewPlayerUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, XyCell);
                    InitSystem.UnitInventorCom.TakeUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
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
