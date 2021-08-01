using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;

internal sealed class CreatorUnitMasterSystem : SystemMasterReduction
{
    internal UnitTypes UnitType => _eMM.CreatorEnt_UnitTypeCom.UnitType;

    public override void Run()
    {
        base.Run();

        if (ResourcesDataUIWorker.CanCreateUnit(UnitType, RpcWorker.InfoFrom.Sender, out bool[] haves))
        {
            ResourcesDataUIWorker.BuyCreateUnit(UnitType, RpcWorker.InfoFrom.Sender);
            InfoUnitsContainer.AddUnitsInInventor(UnitType, RpcWorker.InfoFrom.Sender.IsMasterClient);

            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(RpcWorker.InfoFrom.Sender, haves);
        }
    }
}
