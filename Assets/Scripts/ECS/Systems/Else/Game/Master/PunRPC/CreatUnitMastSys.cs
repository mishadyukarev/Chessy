using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class CreatUnitMastSys : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _mastInfoFilter = default;
    private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
    private EcsFilter<InventorUnitsComponent, InventorResourcesComponent> _inventorFilter = default;
    private EcsFilter<SoundEffectsComp> _soundEffFilt = default;


    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellBuildFilt = default;


    public void Run()
    {
        ref var infoCom = ref _mastInfoFilter.Get1(0);
        ref var amountResCom = ref _inventorFilter.Get2(0);
        ref var unitInventorCom = ref _inventorFilter.Get1(0);
        ref var soundEffecCom = ref _soundEffFilt.Get1(0);


        var unitTypeForCreating = _creatorUnitFilter.Get1(0).UnitTypeForCreating;




        var isMastSender = false;
        if (PhotonNetwork.OfflineMode) isMastSender = WhoseMoveCom.IsMainMove;
        else isMastSender = infoCom.FromInfo.Sender.IsMasterClient;


        var isSettedCity = false;

        foreach (var idx in _cellBuildFilt)
        {
            if (_cellBuildFilt.Get1(idx).IsBuildType(BuildingTypes.City))
            {
                if (_cellBuildFilt.Get2(idx).HaveOwner)
                {
                    if (_cellBuildFilt.Get2(idx).IsMasterClient == isMastSender)
                    {
                        isSettedCity = true;
                    }
                }

                else if (_cellBuildFilt.Get3(idx).HaveLocalPlayer)
                {
                    if (_cellBuildFilt.Get3(idx).IsMainMaster == isMastSender)
                    {
                        isSettedCity = true;
                    }
                }
            }
        }

        if (isSettedCity)
        {
            if (amountResCom.CanCreateUnit(unitTypeForCreating, isMastSender, out bool[] haves))
            {
                amountResCom.BuyCreateUnit(unitTypeForCreating, isMastSender);
                unitInventorCom.AddUnitsInInventor(unitTypeForCreating, isMastSender);

                RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.SoundGoldPack);
            }
            else
            {
                RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.Mistake);
                RpcSys.MistakeEconomyToGeneral(infoCom.FromInfo.Sender, haves);
            }
        }
        else
        {
            RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.Mistake);
            RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, infoCom.FromInfo.Sender);
        }
    }
}
