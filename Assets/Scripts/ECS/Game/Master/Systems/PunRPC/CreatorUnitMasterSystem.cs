using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

internal sealed class CreatorUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;

    public override void Run()
    {
        base.Run();

        if (EconomyManager.CanCreateUnit(UnitType, InfoFrom.Sender, out bool[] haves))
        {
            EconomyManager.CreateUnit(UnitType, InfoFrom.Sender);
            Main.Instance.EGGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);

            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
        }
        else
        {
            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
        }
    }
}
