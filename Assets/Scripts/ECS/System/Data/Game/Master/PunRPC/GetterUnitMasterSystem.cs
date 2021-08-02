using Assets.Scripts;
using Assets.Scripts.Workers;
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
                isGetted = InfoUnitsDataContainer.HaveUnitInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);
                if (isGetted)
                {
                    unitType = UnitType;
                }
                break;

            case UnitTypes.Pawn:
                if (InfoUnitsDataContainer.HaveUnitInInventor(UnitTypes.PawnSword, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InfoUnitsDataContainer.HaveUnitInInventor(UnitTypes.Pawn, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.PawnSword:
                throw new Exception();

            case UnitTypes.Rook:
                if (InfoUnitsDataContainer.HaveUnitInInventor(UnitTypes.RookCrossbow, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InfoUnitsDataContainer.HaveUnitInInventor(UnitTypes.Rook, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType;
                }
                break;

            case UnitTypes.RookCrossbow:
                throw new Exception();

            case UnitTypes.Bishop:
                if (InfoUnitsDataContainer.HaveUnitInInventor(UnitTypes.BishopCrossbow, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
                {
                    isGetted = true;
                    unitType = UnitType + 1;
                }
                else if (InfoUnitsDataContainer.HaveUnitInInventor(UnitTypes.Bishop, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient))
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
        PhotonPunRPC.GetUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender, isGetted, unitType);
    }
}
