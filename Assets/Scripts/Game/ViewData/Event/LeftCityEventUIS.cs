namespace Game.Game
{
    sealed class LeftCityEventUIS : SystemUIAbstract
    {
        internal LeftCityEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftCityEs.BuildE(BuildingTypes.House).ButtonC.AddListener(delegate { Build(BuildingTypes.House); });
            UIEs.LeftCityEs.BuildE(BuildingTypes.Market).ButtonC.AddListener(delegate { Build(BuildingTypes.Market); });
            UIEs.LeftCityEs.BuildE(BuildingTypes.Smelter).ButtonC.AddListener(delegate { Build(BuildingTypes.Smelter); });
        }

        void Build(in BuildingTypes build)
        {
            E.SelectedBuildingTC.Build = build;
            E.CellClickTC.Click = CellClickTypes.CityBuildBuilding;
        }
    }
}