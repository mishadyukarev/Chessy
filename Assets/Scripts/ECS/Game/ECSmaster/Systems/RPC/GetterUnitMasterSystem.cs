using Photon.Pun;


internal sealed class GetterUnitMasterSystem : RPCMasterSystemReduction
{
    private int _amountForTakingUnit = 1;

    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();

        var isGetted = _eGM.UnitInventorEnt_UnitInventorCom.AmountUnits(UnitType, Info.Sender.IsMasterClient) >= _amountForTakingUnit;

        _photonPunRPC.GetUnitToGeneral(Info.Sender, isGetted, UnitType);
    }
}
