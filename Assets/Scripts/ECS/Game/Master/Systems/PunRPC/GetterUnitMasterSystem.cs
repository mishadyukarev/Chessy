using Assets.Scripts;
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
                isGetted = _eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitType, Info.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitTypes.King;
                }
                break;

            case UnitTypes.Pawn:
                if(_eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitTypes.PawnSword, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.PawnSword;
                }
                else if (_eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitTypes.Pawn, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Pawn;
                }
                break;

            case UnitTypes.PawnSword:
                break;

            case UnitTypes.Rook:
                if (_eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitTypes.RookCrossbow, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.RookCrossbow;
                }
                else if (_eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitTypes.Rook, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.Rook;
                }
                break;

            case UnitTypes.RookCrossbow:
                break;

            case UnitTypes.Bishop:
                if (_eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitTypes.BishopCrossbow, Info.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitTypes.BishopCrossbow;
                }
                else if (_eGM.UnitInventorEnt_UnitInventorCom.HaveUnit(UnitTypes.Bishop, Info.Sender.IsMasterClient))
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
