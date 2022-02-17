namespace Game.Game
{
    sealed class LeftZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftZonesUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxC.Idx;

            UIEs.LeftCityEs.Zone.SetActive(false);
            UIEs.LeftEnvEs.Zone.SetActive(false);
            UIEs.LeftMarketEs.Zone.SetActive(false);
            UIEs.LeftSmelterEs.Zone.SetActive(false);

            if (Es.SelectedIdxC.Idx > 0)
            {
                if (Es.BuildTC(idx_sel).HaveBuilding)
                {
                    if (Es.BuildPlayerTC(idx_sel).Is(Es.CurPlayerI.Player))
                    {
                        if (Es.BuildTC(idx_sel).Is(BuildingTypes.City))
                        {
                            UIEs.LeftCityEs.Zone.SetActive(true);
                        }
                        else if (Es.BuildTC(idx_sel).Is(BuildingTypes.Market))
                        {
                            UIEs.LeftMarketEs.Zone.SetActive(true);
                        }
                        else if (Es.BuildTC(idx_sel).Is(BuildingTypes.Smelter))
                        {
                            UIEs.LeftSmelterEs.Zone.SetActive(true);
                        }
                    }

                    if (Es.BuildTC(idx_sel).Is(BuildingTypes.Farm) || Es.BuildTC(idx_sel).Is(BuildingTypes.Woodcutter))
                    {
                        UIEs.LeftEnvEs.Zone.SetActive(true);
                    }
                }
                else
                {
                    UIEs.LeftEnvEs.Zone.SetActive(true);
                }
            }
        }
    }
}