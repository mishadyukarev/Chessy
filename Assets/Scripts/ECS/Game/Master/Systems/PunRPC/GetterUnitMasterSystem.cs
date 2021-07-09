using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;


internal sealed class GetterUnitMasterSystem : RPCMasterSystemReduction
{
    private int _amountForTakingUnit = 1;

    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

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
                isGetted = _eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitType, Info.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitTypes.King;
                }
                break;

            case UnitTypes.Pawn:
                if(_eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitTypes.PawnSword, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.PawnSword;
                }
                else if (_eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitTypes.Pawn, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Pawn;
                }
                break;

            case UnitTypes.PawnSword:
                break;

            case UnitTypes.Rook:
                if (_eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitTypes.RookCrossbow, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.RookCrossbow;
                }
                else if (_eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitTypes.Rook, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Rook;
                }
                break;

            case UnitTypes.RookCrossbow:
                break;

            case UnitTypes.Bishop:
                if (_eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitTypes.BishopCrossbow, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.BishopCrossbow;
                }
                else if (_eGM.UnitInfoEnt_UnitInventorCom.HaveUnitInInventor(UnitTypes.Bishop, Info.Sender.IsMasterClient))
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
        _photonPunRPC.GetUnitToGeneral(Info.Sender, isGetted, unitType);
    }
}
