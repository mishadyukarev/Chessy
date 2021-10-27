using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class BuildZoneUISys : IEcsRunSystem
    {
        private EcsFilter<BuildLeftZoneViewUICom> _buildZoneUIFilter = default;

        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;

        public void Run()
        {
            ref var selUnitDataCom = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);
            ref var selOwnUnitCom = ref _cellBuildFilter.Get2(SelectorC.IdxSelCell);

            ref var buildZoneViewCom = ref _buildZoneUIFilter.Get1(0);


            if (SelectorC.IsSelCell && selUnitDataCom.Is(BuildingTypes.City))
            {
                if (selOwnUnitCom.IsMine)
                {
                    //buildZoneViewCom.SetTextMelt(LanguageComCom.GetText(GameLanguageTypes.Melt));
                    //buildZoneViewCom.SetTextUpgrade(BuildingTypes.Farm, LanguageComCom.GetText(GameLanguageTypes.UpgradeFarm));
                    //buildZoneViewCom.SetTextUpgrade(BuildingTypes.Woodcutter, LanguageComCom.GetText(GameLanguageTypes.UpgradeWoodcutter));
                    //buildZoneViewCom.SetTextUpgrade(BuildingTypes.Mine, LanguageComCom.GetText(GameLanguageTypes.UpgradeMine));

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
}