using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;

internal sealed class BuildZoneUISys : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<BuildLeftZoneViewUICom> _buildZoneUIFilter = default;

    private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);

        ref var selUnitDataCom = ref _cellBuildFilter.Get1(selCom.IdxSelCell);
        ref var selOwnUnitCom = ref _cellBuildFilter.Get2(selCom.IdxSelCell);

        ref var buildZoneViewCom = ref _buildZoneUIFilter.Get1(0);


        if (selCom.IsSelectedCell && selUnitDataCom.IsBuildType(BuildingTypes.City))
        {
            if (selOwnUnitCom.IsMine)
            {
                buildZoneViewCom.SetTextMelt(LanguageComComp.GetText(GameLanguageTypes.Melt));
                buildZoneViewCom.SetTextUpgrade(BuildingTypes.Farm, LanguageComComp.GetText(GameLanguageTypes.UpgradeFarm));
                buildZoneViewCom.SetTextUpgrade(BuildingTypes.Woodcutter, LanguageComComp.GetText(GameLanguageTypes.UpgradeWoodcutter));
                buildZoneViewCom.SetTextUpgrade(BuildingTypes.Mine, LanguageComComp.GetText(GameLanguageTypes.UpgradeMine));

                buildZoneViewCom.SetActiveZone(true);
            }
            else _buildZoneUIFilter.Get1(0).SetActiveZone(false);
        }
        else
        {
            buildZoneViewCom.SetActiveZone(false);
        }
    }
}
