namespace Game.Game
{
    sealed class UpdateFireMS : SystemCellAbstract, IEcsRunSystem
    {
        public UpdateFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = UnitEs;

            CellEs.TryGetIdxAround(Es.WindE.CenterCloud.Idx, out var directs);

            foreach (var item in directs)
            {
                CellEs.FireEs.Fire(item.Value).Fire.Disable();
            }


            foreach (byte idx_0 in CellEs.Idxs)
            {
                var xy_0 = CellEs.CellE(idx_0).XyC.Xy;

                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;

                var hpUnit_0 = UnitEs.StatEs.Hp(idx_0).Health;

                var buil_0 = BuildEs.BuildingE(idx_0).BuildTC;
                var ownBuil_0 = BuildEs.BuildingE(idx_0).Owner;

                ref var fire_0 = ref CellEs.FireEs.Fire(idx_0).Fire;

                if (fire_0.Have)
                {
                    CellEs.EnvironmentEs.AdultForest(idx_0).Fire();

                    if (unit_0.Have)
                    {
                        UnitEs.StatEs.Hp(idx_0).Health.Amount -= UnitDamageValues.FIRE_DAMAGE;
                        if (!UnitEs.StatEs.Hp(idx_0).Health.Have)
                        {
                            unitEs.Main(idx_0).Kill(Es);
                        }
                    }



                    if (!CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                    {
                        Es.WhereBuildingEs.HaveBuild(BuildEs.BuildingE(idx_0), idx_0).HaveBuilding.Have = false;
                        BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);

                        CellEs.EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            CellEs.EnvironmentEs.YoungForest(idx_0).SetNew(Es.WhereEnviromentEs);
                        }


                        fire_0.Disable();


                        foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                        {
                            if (CellEs.ParentE(idx_1).IsActiveSelf.IsActive)
                            {
                                if (CellEs.EnvironmentEs.AdultForest(idx_1).HaveEnvironment)
                                {
                                    CellEs.FireEs.Fire(idx_1).Fire.Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}