namespace Chessy.Game
{
    sealed class LeftCityUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftCityUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var whoseMove = E.WhoseMove.Player;

            UIE.LeftEs.CityE(BuildingTypes.Market).CostGOC.SetActive(!E.PlayerE(whoseMove).HaveMarket);
            UIE.LeftEs.CityE(BuildingTypes.Smelter).CostGOC.SetActive(!E.PlayerE(whoseMove).HaveSmelter);
        }
    }
}