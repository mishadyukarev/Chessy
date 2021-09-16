using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Supports;
using Leopotam.Ecs;

internal sealed class CreatorUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _mastInfoFilter = default;
    private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
    private EcsFilter<InventorUnitsComponent, InventorResourcesComponent> _inventorFilter = default;

    private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellBuildFilter = default;


    public void Run()
    {
        ref var infoCom = ref _mastInfoFilter.Get1(0);
        ref var amountResCom = ref _inventorFilter.Get2(0);
        ref var unitInventorCom = ref _inventorFilter.Get1(0);

        var unitTypeForCreating = _creatorUnitFilter.Get1(0).UnitTypeForCreating;


        if (_cellBuildFilter.IsSettedCity(infoCom.FromInfo.Sender.IsMasterClient))
        {
            if (amountResCom.CanCreateUnit(unitTypeForCreating, infoCom.FromInfo.Sender, out bool[] haves))
            {
                amountResCom.BuyCreateUnit(unitTypeForCreating, infoCom.FromInfo.Sender);
                unitInventorCom.AddUnitsInInventor(unitTypeForCreating, infoCom.FromInfo.Sender.IsMasterClient);

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
