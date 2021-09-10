using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

internal sealed class CreatorUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _mastInfoFilter = default;
    private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;
    private EcsFilter<InventorUnitsComponent, InventorResourcesComponent> _inventorFilter = default;

    private EcsFilter<CellBuildDataComponent, OwnerComponent> _cellBuildFilter = default;

    private UnitTypes UnitType => _creatorUnitFilter.Get1(0).UnitTypeForCreating;

    public void Run()
    {
        ref var infoCom = ref _mastInfoFilter.Get1(0);
        ref var amountResCom = ref _inventorFilter.Get2(0);
        ref var unitInventorCom = ref _inventorFilter.Get1(0);

        foreach (var idxCell in _cellBuildFilter)
        {
            if (_cellBuildFilter.Get1(idxCell).IsBuildType(BuildingTypes.City))
            {
                if (_cellBuildFilter.Get2(idxCell).IsHim(infoCom.FromInfo.Sender))
                {
                    if (amountResCom.CanCreateUnit(UnitType, infoCom.FromInfo.Sender, out bool[] haves))
                    {
                        amountResCom.BuyCreateUnit(UnitType, infoCom.FromInfo.Sender);
                        unitInventorCom.AddUnitsInInventor(UnitType, infoCom.FromInfo.Sender.IsMasterClient);

                        RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        RpcSys.SoundToGeneral(infoCom.FromInfo.Sender, SoundEffectTypes.Mistake);
                        RpcSys.MistakeEconomyToGeneral(infoCom.FromInfo.Sender, haves);
                    }

                    return;
                }
            }
        }

        RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, infoCom.FromInfo.Sender);
    }
}
