using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class CreateUnitMastSys : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _mastInfoFilter = default;
    private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
    private EcsFilter<InventorUnitsComponent, InventResourCom> _inventorFilter = default;
    private EcsFilter<SoundEffectsComp> _soundEffFilt = default;


    private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilt = default;


    public void Run()
    {
        ref var infoCom = ref _mastInfoFilter.Get1(0);
        ref var amountResCom = ref _inventorFilter.Get2(0);
        ref var unitInventorCom = ref _inventorFilter.Get1(0);
        ref var soundEffecCom = ref _soundEffFilt.Get1(0);


        var unitTypeForCreating = _creatorUnitFilter.Get1(0).UnitTypeForCreating;


        PlayerTypes playerSender = default;
        if (GameModesCom.IsOffMode) playerSender = WhoseMoveCom.CurOfflinePlayer;
        else playerSender = infoCom.FromInfo.Sender.GetPlayerType();


        var isSettedCity = false;

        foreach (var idx in _cellBuildFilt)
        {
            if (_cellBuildFilt.Get1(idx).IsBuildType(BuildingTypes.City))
            {
                if (_cellBuildFilt.Get2(idx).IsPlayerType(playerSender))
                {
                    isSettedCity = true;
                }
            }
        }

        if (isSettedCity)
        {
            if (amountResCom.CanCreateUnit(playerSender, unitTypeForCreating, out bool[] haves))
            {
                amountResCom.BuyCreateUnit(playerSender, unitTypeForCreating);
                unitInventorCom.AddUnitsInInventor(playerSender, unitTypeForCreating);

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
