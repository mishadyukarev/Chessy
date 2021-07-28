using Assets.Scripts;
using Assets.Scripts.Workers.Game.Else;
using Photon.Pun;
using System;

internal sealed class GetterUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;
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
                isGetted = InventorUnitsDataWorker.HaveUnitInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitType;
                }
                break;

            case UnitTypes.Pawn:
                if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.PawnSword, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.Pawn, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.PawnSword:
                throw new Exception();

            case UnitTypes.Rook:
                if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.RookCrossbow, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.Rook, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.RookCrossbow:
                throw new Exception();

            case UnitTypes.Bishop:
                if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.BishopCrossbow, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.Bishop, InfoFrom.Sender.IsMasterClient))
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
        PhotonPunRPC.GetUnitToGeneral(InfoFrom.Sender, isGetted, unitType);
    }
}
