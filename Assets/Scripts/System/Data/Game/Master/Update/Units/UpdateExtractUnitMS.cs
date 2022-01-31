namespace Game.Game
{
    sealed class UpdateExtractUnitMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateExtractUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < CellEs.Count; idx_0++)
            {
                if (UnitEs.Main(idx_0).CanExtractPawnAdultForest(UnitEs.StatEs, EnvironmentEs))
                {
                    EnvironmentEs.AdultForest(idx_0).ExtractPawn(UnitEs, Es.InventorResourcesEs);

                    if (EnvironmentEs.AdultForest(idx_0).HaveEnvironment)
                    {
                        if (BuildEs.BuildingE(idx_0).BuildTC.Is(BuildingTypes.Camp) || !BuildEs.BuildingE(idx_0).BuildTC.Have)
                        {
                            BuildEs.BuildingE(idx_0).SetNew(BuildingTypes.Woodcutter, UnitEs.Main(idx_0).OwnerC.Player, BuildEs, Es.WhereBuildingEs);
                        }

                        else if (!BuildEs.BuildingE(idx_0).BuildTC.Is(BuildingTypes.Woodcutter))
                        {
                            UnitEs.Main(idx_0).SetCondition(ConditionUnitTypes.Protected);
                        }
                    }
                    else
                    {
                        BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);
                        EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);

                        EnvironmentEs.YoungForest(idx_0).SetNew(Es.WhereEnviromentEs);
                    }
                }
                //else if (!Unit<UnitCellEC>(idx_0).CanResume(out resume, out env))
                //{
                //    if (EntPool.CellUnitHpEs.HaveMax(idx_0))
                //    {
                //        if (unit_0.Have && EntitiesPool.CellUnitStepEs.HaveMin(idx_0))
                //        {
                //            condUnit_0.Condition = ConditionUnitTypes.Protected;
                //        }
                //    }
                //}
            }
        }
    }
}