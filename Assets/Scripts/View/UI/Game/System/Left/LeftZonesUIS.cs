using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class LeftZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame _eUI;

        internal LeftZonesUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            _eUI = entsUI;
        }

        public void Run()
        {
            _eUI.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(false);
            _eUI.LeftEnvEs.Zone.SetActive(false);

            if (e.IsSelectedCity)
            {
                _eUI.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(true);
            }
            else
            {
                var idx_sel = e.CellsC.Selected;

                if (e.CellsC.IsSelectedCell)
                {
                    _eUI.LeftEnvEs.Zone.SetActive(true);
                }
            }
        }
    }
}