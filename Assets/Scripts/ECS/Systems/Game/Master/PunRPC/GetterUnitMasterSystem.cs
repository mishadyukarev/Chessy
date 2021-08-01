using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using System;

internal sealed class GetterUnitMasterSystem : SystemMasterReduction
{
    internal UnitTypes UnitType => _eMM.CreatorEnt_UnitTypeCom.UnitType;

    public override void Run()
    {
        base.Run();

        bool isGetted = false; //= _eGM.UnitInventorEnt_UnitInventorCom.AmountUnits(UnitType, Info.Sender.IsMasterClient) >= _amountForTakingUnit;
        UnitTypes unitType = UnitTypes.None;
        switch (UnitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                isGetted = InfoUnitsContainer.HaveUnitInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitType;
                }
                break;

            case UnitTypes.Pawn:
                if (InfoUnitsContainer.HaveUnitInInventor(UnitTypes.PawnSword, RpcWorker.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InfoUnitsContainer.HaveUnitInInventor(UnitTypes.Pawn, RpcWorker.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.PawnSword:
                throw new Exception();

            case UnitTypes.Rook:
                if (InfoUnitsContainer.HaveUnitInInventor(UnitTypes.RookCrossbow, RpcWorker.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InfoUnitsContainer.HaveUnitInInventor(UnitTypes.Rook, RpcWorker.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.RookCrossbow:
                throw new Exception();

            case UnitTypes.Bishop:
                if (InfoUnitsContainer.HaveUnitInInventor(UnitTypes.BishopCrossbow, RpcWorker.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InfoUnitsContainer.HaveUnitInInventor(UnitTypes.Bishop, RpcWorker.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.BishopCrossbow:
                throw new Exception();

            default:
                throw new Exception();
        }
        PhotonPunRPC.GetUnitToGeneral(RpcWorker.InfoFrom.Sender, isGetted, unitType);
    }
}
