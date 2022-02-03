namespace Game.Game
{
    sealed class SetIceWallSnowyMS : SystemCellAbstract, IEcsRunSystem
    {
        internal SetIceWallSnowyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.MasterEs.IceWallSnowyME.IdxC.Idx;
            var ability = Es.MasterEs.AbilityC.Ability;


            if (UnitStatEs(idx_0).WaterE.Have(ability) || RiverEs(idx_0).River.HaveRiverNear)
            {
                if (!BuildEs(idx_0).BuildingE.HaveBuilding)
                {
                    EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);
                    EnvironmentEs(idx_0).Fertilizer.Destroy(Es.WhereEnviromentEs);

                    if (UnitStatEs(idx_0).StepE.Have(ability))
                    {
                        UnitStatEs(idx_0).StepE.Take(ability);

                        UnitEs(idx_0).CooldownAbility(ability).SetAfterAbility();

                        BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.IceWall, UnitEs(idx_0).MainE.OwnerC.Player, BuildEs(idx_0), Es.WhereBuildingEs);
                        Es.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, UnitEs(idx_0).MainE.OwnerC.Player, idx_0).HaveBuilding.Have = true;
                    }
                }
            }
        }
    }
}