using Photon.Pun;

internal sealed class CreatorUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;

    public override void Run()
    {
        base.Run();

        if(_eM.CanCreateUnit(UnitType, Info.Sender, out bool[] haves))
        {
            _eM.CreateUnit(UnitType, Info.Sender);
        }
        else _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
    }
}
