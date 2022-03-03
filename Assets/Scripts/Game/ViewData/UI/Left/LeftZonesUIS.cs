namespace Chessy.Game
{
    sealed class LeftZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftZonesUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            var idx_sel = E.SelectedIdxC.Idx;

            UIE.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(false);
            UIE.LeftEnvEs.Zone.SetActive(false);

            if (E.SelectedIdxC.Idx > 0)
            {
                if (E.BuildingTC(idx_sel).HaveBuilding)
                {
                    if (E.BuildingPlayerTC(idx_sel).Is(E.CurPlayerITC.Player))
                    {
                        if (E.BuildingTC(idx_sel).Is(BuildingTypes.City))
                        {
                            UIE.LeftEs.CityE(BuildingTypes.House).Parent.SetActive(true);
                        }
                    }

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