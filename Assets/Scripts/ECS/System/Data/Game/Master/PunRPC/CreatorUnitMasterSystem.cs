using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;

internal sealed class CreatorUnitMasterSystem : SystemMasterReduction
{
    internal UnitTypes UnitType => _eMM.CreatorEnt_UnitTypeCom.UnitType;

    public override void Run()
    {
        base.Run();

        if (ResourcesUIDataContainer.CanCreateUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender, out bool[] haves))
        {
            ResourcesUIDataContainer.BuyCreateUnit(UnitType, RpcMasterDataContainer.InfoFrom.Sender);
            InitSystem.UnitInventorCom.AddUnitsInInventor(UnitType, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient);

            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
        }
        else
        {
            PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
            PhotonPunRPC.MistakeEconomyToGeneral(RpcMasterDataContainer.InfoFrom.Sender, haves);
        }
    }
}
