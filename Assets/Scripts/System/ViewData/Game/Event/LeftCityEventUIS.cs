using static Game.Game.LeftCityUIEs;

namespace Game.Game
{
    sealed class LeftCityEventUIS : SystemViewAbstract
    {
        internal LeftCityEventUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
            VEs.UIEs.LeftEs.CityEs.BuildE(BuildingTypes.House).ButtonCRef.AddListener(delegate { Build(BuildingTypes.House); });
            VEs.UIEs.LeftEs.CityEs.BuildE(BuildingTypes.Market).ButtonCRef.AddListener(delegate { Build(BuildingTypes.Market); });
            VEs.UIEs.LeftEs.CityEs.BuildE(BuildingTypes.Smelter).ButtonCRef.AddListener(delegate { Build(BuildingTypes.Smelter); });
        }

        void Build(in BuildingTypes build)
        {
            Es.SelectedBuildingE.Set(build, Es.ClickerObjectE);
        }
    }
}