using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
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
            Main.Instance.EGGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitType, Info.Sender.IsMasterClient);

            _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.SoundGoldPack);
        }
        else
        {
            _photonPunRPC.SoundToGeneral(Info.Sender, SoundEffectTypes.Mistake);
            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
        }
    }
}
