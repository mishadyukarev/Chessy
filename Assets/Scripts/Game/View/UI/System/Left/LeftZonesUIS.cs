using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    sealed class LeftZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftZonesUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            eUI.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(false);
            eUI.LeftEnvEs.Zone.SetActive(false);

            if (eMGame.IsSelectedCity)
            {
                eUI.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(true);
            }
            else
            {
                var idx_sel = eMGame.CellsC.Selected;

                if (eMGame.CellsC.Selected > 0)
                {
                    if (eMGame.BuildingTC(idx_sel).HaveBuilding)
                    {

                        if (eMGame.BuildingTC(idx_sel).Is(BuildingTypes.Farm) || eMGame.BuildingTC(idx_sel).Is(BuildingTypes.Woodcutter))
                        {
                            eUI.LeftEnvEs.Zone.SetActive(true);
                        }
                    }
                    else
                    {
                        eUI.LeftEnvEs.Zone.SetActive(true);
                    }
                }
            }
        }
    }
}