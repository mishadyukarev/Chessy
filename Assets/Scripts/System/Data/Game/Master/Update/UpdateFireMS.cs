namespace Game.Game
{
    sealed class UpdateFireMS : SystemCellAbstract, IEcsRunSystem
    {
        public UpdateFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var unitEs = Es.CellEs.UnitEs;

            Es.CellEs.TryGetIdxAround(Es.WindE.CenterCloud.Idx, out var directs);

            foreach (var item in directs)
            {
                Es.CellEs.FireEs.Fire(item.Value).Fire.Disable();
            }


            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                var xy_0 = Es.CellEs.CellE(idx_0).XyC.Xy;

                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;

                ref var hpUnit_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;

                ref var buil_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuil_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref Es.CellEs.FireEs.Fire(idx_0).Fire;

                if (fire_0.Have)
                {
                    Es.CellEs.EnvironmentEs.AdultForest(idx_0).Fire();

                    if (unit_0.Have)
                    {
                        Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Take(UnitDamageValues.FIRE_DAMAGE);
                        if (!Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Have)
                        {
                            unitEs.Kill(idx_0, Es);
                        }
                    }



                    if (!Es.CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                    {
                        Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                        Es.CellEs.BuildEs.Build(idx_0).Remove();

                        Es.CellEs.EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            Es.CellEs.EnvironmentEs.YoungForest(idx_0).SetNew(Es.WhereEnviromentEs);
                        }


                        fire_0.Disable();


                        foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                        {
                            if (Es.CellEs.ParentE(idx_1).IsActiveSelf.IsActive)
                            {
                                if (Es.CellEs.EnvironmentEs.AdultForest(idx_1).HaveEnvironment)
                                {
                                    Es.CellEs.FireEs.Fire(idx_1).Fire.Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}