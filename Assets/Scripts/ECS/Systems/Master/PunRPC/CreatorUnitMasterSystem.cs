using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Info;
using Photon.Pun;

internal sealed class CreatorUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;
    internal UnitTypes UnitType => _eMM.CreatorEnt_UnitTypeCom.UnitType;

    public override void Run()
    {
        base.Run();

        if (InfoResourcesDataWorker.CanCreateUnit(UnitType, InfoFrom.Sender, out bool[] haves))
        {
            InfoResourcesDataWorker.BuyCreateUnit(UnitType, InfoFrom.Sender);
            InventorUnitsDataWorker.AddUnitsInInventor(UnitType, InfoFrom.Sender.IsMasterClient);

            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
        }
    }
}
