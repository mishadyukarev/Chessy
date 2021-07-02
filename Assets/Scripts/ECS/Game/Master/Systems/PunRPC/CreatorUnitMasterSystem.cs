using Assets.Scripts;
using Photon.Pun;

internal sealed class CreatorUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;

    public override void Run()
    {
        base.Run();

        if (EconomyManager.CanCreateUnit(UnitType, Info.Sender, out bool[] haves))
        {
            EconomyManager.CreateUnit(UnitType, Info.Sender);
        }
        else _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
    }
}
