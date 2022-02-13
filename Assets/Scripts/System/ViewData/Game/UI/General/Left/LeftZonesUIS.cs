namespace Game.Game
{
    sealed class LeftZonesUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal LeftZonesUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.Idx;

            UIEs.LeftCityEs.Zone.SetActive(false);
            UIEs.LeftEnvEs.Zone.SetActive(false);
            UIEs.LeftMarketEs.Zone.SetActive(false);
            UIEs.LeftSmelterEs.Zone.SetActive(false);

            if (Es.SelectedIdxE.IsSelCell)
            {
                if (Es.BuildingE(idx_sel).HaveBuilding)
                {
                    if (Es.BuildingE(idx_sel).Is(Es.WhoseMoveE.CurPlayerI))
                    {
                        if (Es.BuildingE(idx_sel).Is(BuildingTypes.City))
                        {
                            UIEs.LeftCityEs.Zone.SetActive(true);
                        }
                        else if (Es.BuildingE(idx_sel).Is(BuildingTypes.Market))
                        {
                            UIEs.LeftMarketEs.Zone.SetActive(true);
                        }
                        else if (Es.BuildingE(idx_sel).Is(BuildingTypes.Smelter))
                        {
                            UIEs.LeftSmelterEs.Zone.SetActive(true);
                        }
                    }

                    if (Es.BuildingE(idx_sel).Is(BuildingTypes.Farm) || Es.BuildingE(idx_sel).Is(BuildingTypes.Woodcutter))
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