namespace Game.Game
{
    struct UpdateFireMS : IEcsRunSystem
    {
        public void Run()
        {
            CellSpaceSupport.TryGetIdxAround(Entities.WindE.CenterCloud.Idx, out var directs);

            foreach (var item in directs)
            {
                Entities.CellEs.FireEs.Fire(item.Value).Fire.Disable();
            }


            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                var xy_0 = Entities.CellEs.CellE(idx_0).XyC.Xy;

                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

                ref var hpUnit_0 = ref Entities.CellEs.UnitEs.Hp(idx_0).AmountC;

                ref var buil_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuil_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;

                if (fire_0.Have)
                {
                    Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0)
                        .Resources.Take(CellEnvironmentValues.MaxResources(EnvironmentTypes.AdultForest) / 2);

                    if (unit_0.Have)
                    {
                        Entities.CellEs.UnitEs.Hp(idx_0).AmountC.Take(UnitDamageValues.FIRE_DAMAGE);
                        if (!Entities.CellEs.UnitEs.Hp(idx_0).AmountC.Have)
                        {
                            Entities.CellEs.UnitEs.Kill(idx_0);
                        }
                    }



                    if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                    {
                        Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                        Entities.CellEs.BuildEs.Build(idx_0).Remove();

                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Remove();


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).SetNew();
                        }


                        fire_0.Disable();


                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (Entities.CellEs.ParentE(idx_1).IsActiveSelf.IsActive)
                            {
                                if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_1).Resources.Have)
                                {
                                    Entities.CellEs.FireEs.Fire(idx_1).Fire.Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}