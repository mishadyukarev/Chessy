using Assets.Scripts;
using Photon.Pun;

internal sealed class UpgradeBuildingMasterSystem : RPCMasterSystemReduction
{
    internal BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

    public override void Run()
    {
        base.Run();

        if(EconomyManager.CanUpgradeBuildings(Info.Sender, BuildingType, out bool[] haves))
        {
            EconomyManager.UpgradeBuildings(Info.Sender, BuildingType);
        }
        else
        {
            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
        }
    }
}
