using Assets.Scripts;
using Assets.Scripts.Workers.Game.Else;
using Photon.Pun;


internal sealed class GetterUnitMasterSystem : RPCMasterSystemReduction
{
    private int _amountForTakingUnit = 1;

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
                break;

            case UnitTypes.King:
                isGetted = InventorUnitsDataWorker.HaveUnitInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitTypes.King;
                }
                break;

            case UnitTypes.Pawn:
                if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.PawnSword, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.PawnSword;
                }
                else if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.Pawn, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Pawn;
                }
                break;

            case UnitTypes.PawnSword:
                break;

            case UnitTypes.Rook:
                if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.RookCrossbow, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.RookCrossbow;
                }
                else if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.Rook, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Rook;
                }
                break;

            case UnitTypes.RookCrossbow:
                break;

            case UnitTypes.Bishop:
                if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.BishopCrossbow, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.BishopCrossbow;
                }
                else if (InventorUnitsDataWorker.HaveUnitInInventor(UnitTypes.Bishop, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Bishop;
                }
                break;

            case UnitTypes.BishopCrossbow:
                break;

            default:
                break;
        }
        PhotonPunRPC.GetUnitToGeneral(InfoFrom.Sender, isGetted, unitType);
    }
}
