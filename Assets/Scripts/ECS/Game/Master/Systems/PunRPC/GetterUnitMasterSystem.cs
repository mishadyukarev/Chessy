using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;
using Photon.Pun;


internal sealed class GetterUnitMasterSystem : RPCMasterSystemReduction
{
    private int _amountForTakingUnit = 1;

    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
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
                isGetted = InfoUnitsWorker.HaveUnitInInventor(UnitType, InfoFrom.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitTypes.King;
                }
                break;

            case UnitTypes.Pawn:
                if (InfoUnitsWorker.HaveUnitInInventor(UnitTypes.PawnSword, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.PawnSword;
                }
                else if (InfoUnitsWorker.HaveUnitInInventor(UnitTypes.Pawn, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Pawn;
                }
                break;

            case UnitTypes.PawnSword:
                break;

            case UnitTypes.Rook:
                if (InfoUnitsWorker.HaveUnitInInventor(UnitTypes.RookCrossbow, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.RookCrossbow;
                }
                else if (InfoUnitsWorker.HaveUnitInInventor(UnitTypes.Rook, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Rook;
                }
                break;

            case UnitTypes.RookCrossbow:
                break;

            case UnitTypes.Bishop:
                if (InfoUnitsWorker.HaveUnitInInventor(UnitTypes.BishopCrossbow, InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.BishopCrossbow;
                }
                else if (InfoUnitsWorker.HaveUnitInInventor(UnitTypes.Bishop, InfoFrom.Sender.IsMasterClient))
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
