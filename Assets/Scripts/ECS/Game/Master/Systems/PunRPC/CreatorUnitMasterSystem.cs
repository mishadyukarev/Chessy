using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

internal sealed class CreatorUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
    internal UnitTypes UnitType => _eMM.CreatorEnt_UnitTypeCom.UnitType;

    public override void Run()
    {
        base.Run();

        if (EconomyWorker.CanCreateUnit(UnitType, InfoFrom.Sender, out bool[] haves))
        {
            EconomyWorker.CreateUnit(UnitType, InfoFrom.Sender);
            Main.Instance.EntGGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);

            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
        }
    }
}
