namespace Game.Game
{
    sealed class LeftCityEventUIS : SystemUIAbstract
    {
        internal LeftCityEventUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftCityEs.BuildE(BuildingTypes.House).ButtonCRef.AddListener(delegate { Build(BuildingTypes.House); });
            UIEs.LeftCityEs.BuildE(BuildingTypes.Market).ButtonCRef.AddListener(delegate { Build(BuildingTypes.Market); });
            UIEs.LeftCityEs.BuildE(BuildingTypes.Smelter).ButtonCRef.AddListener(delegate { Build(BuildingTypes.Smelter); });
        }

        void Build(in BuildingTypes build)
        {
            Es.SelectedBuildingE.Set(build, Es.ClickerObjectE);
        }
    }
}