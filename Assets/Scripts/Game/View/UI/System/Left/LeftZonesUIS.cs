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
            UIE.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(false);
            UIE.LeftEnvEs.Zone.SetActive(false);

            if (E.IsSelectedCity)
            {
                UIE.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(true);
            }
            else
            {
                var idx_sel = E.CellsC.Selected;

                if (E.CellsC.Selected > 0)
                {
                    if (E.BuildingTC(idx_sel).HaveBuilding)
                    {

                        if (E.BuildingTC(idx_sel).Is(BuildingTypes.Farm) || E.BuildingTC(idx_sel).Is(BuildingTypes.Woodcutter))
                        {
                            UIE.LeftEnvEs.Zone.SetActive(true);
                        }
                    }
                    else
                    {
                        UIE.LeftEnvEs.Zone.SetActive(true);
                    }
                }
            }
        }
    }
}