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
                if (Es.EnvAdultForestE(idx_0).CanExtractPawn(UnitEs(idx_0)))
                {
                    Es.EnvAdultForestE(idx_0).ExtractPawn(UnitEs(idx_0), Es.InventorResourcesEs);

                    if (Es.EnvAdultForestE(idx_0).HaveEnvironment)
                    {
                        if (BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.Camp) || !BuildEs(idx_0).BuildingE.BuildTC.Have)
                        {
                            BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Woodcutter, UnitEs(idx_0).OwnerE.OwnerC.Player);
                        }

                        else if (!BuildEs(idx_0).BuildingE.BuildTC.Is(BuildingTypes.Woodcutter))
                        {
                            UnitEs(idx_0).ConditionE.Set(ConditionUnitTypes.Protected);
                        }
                    }
                    else
                    {
                        BuildEs(idx_0).BuildingE.Destroy();
                        Es.EnvAdultForestE(idx_0).Destroy(TrailEs(idx_0).Trails);

                        EnvironmentEs(idx_0).YoungForest.SetRandomResources();
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