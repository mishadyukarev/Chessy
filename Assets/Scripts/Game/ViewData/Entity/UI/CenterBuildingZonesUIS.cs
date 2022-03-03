namespace Chessy.Game
{
    sealed class CenterBuildingZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterBuildingZonesUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            UIE.CenterEs.MarketE.Zone.SetActive(false);

            if (E.SelectedBuildingsC.Is(BuildingTypes.Market))
            {
                UIE.CenterEs.MarketE.Zone.SetActive(true);
            }
        }
    }
}