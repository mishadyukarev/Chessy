namespace Game.Game
{
    sealed class UpdateFireMS : SystemCellAbstract, IEcsRunSystem
    {
        internal UpdateFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            CellEsWorker.TryGetIdxAround(Es.WindE.CenterCloud.Idx, out var directs);

            foreach (var item in directs)
            {
                EffectEs(item.Value).FireE.Disable();
            }


            foreach (byte idx_0 in CellEsWorker.Idxs)
            {
                var xy_0 = CellEs(idx_0).CellE.XyC.Xy;

                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                var hpUnit_0 = UnitStatEs(idx_0).Hp.Health;

                var buil_0 = BuildEs(idx_0).BuildingE.BuildTC;
                var ownBuil_0 = BuildEs(idx_0).BuildingE.Owner;

                if (EffectEs(idx_0).FireE.HaveFireC.Have)
                {
                    EnvironmentEs(idx_0).AdultForest.Fire();

                    if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                    {
                        UnitStatEs(idx_0).Hp.Fire(Es);
                    }

                    if (!EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);

                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            EnvironmentEs(idx_0).YoungForest.SetNew(Es.WhereEnviromentEs);
                        }


                        EffectEs(idx_0).FireE.Disable();


                        foreach (var idx_1 in CellEsWorker.GetIdxsAround(idx_0))
                        {
                            if (CellEs(idx_1).ParentE.IsActiveSelf.IsActive)
                            {
                                if (EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                                {
                                    EffectEs(idx_1).FireE.Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}