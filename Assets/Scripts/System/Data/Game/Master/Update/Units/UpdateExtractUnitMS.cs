namespace Game.Game
{
    sealed class UpdateExtractUnitMS : SystemAbstract, IEcsRunSystem
    {
        public UpdateExtractUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (UnitEs(idx_0).MainE.CanExtractPawnAdultForest(UnitEs(idx_0).StatEs, EnvironmentEs(idx_0)))
                {
                    EnvironmentEs(idx_0).AdultForest.ExtractPawn(UnitEs(idx_0), Es.InventorResourcesEs);

                    if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        if (BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.Camp) || !BuildEs(idx_0).BuildingE.BuildTC.Have)
                        {
                            BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Woodcutter, UnitEs(idx_0).MainE.OwnerC.Player, BuildEs(idx_0), Es.WhereBuildingEs);
                        }

                        else if (!BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.Woodcutter))
                        {
                            UnitEs(idx_0).MainE.SetCondition(ConditionUnitTypes.Protected);
                        }
                    }
                    else
                    {
                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);
                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);

                        EnvironmentEs(idx_0).YoungForest.SetNewRandom(Es.WhereEnviromentEs);
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