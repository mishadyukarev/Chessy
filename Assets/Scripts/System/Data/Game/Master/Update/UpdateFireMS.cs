using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpdateFireMS : IEcsRunSystem
    {
        public void Run()
        {
            CellSpaceSupport.TryGetIdxAround(Entities.WindE.CenterCloud.Idx, out var directs);

            foreach (var item in directs)
            {
                CellFireEs.Fire(item.Value).Fire.Disable();
            }


            foreach (byte idx_0 in Idxs)
            {
                var xy_0 = Cell(idx_0).XyC.Xy;

                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var levUnit_0 = ref CellUnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

                ref var hpUnit_0 = ref CellUnitEs.Hp(idx_0).AmountC;

                ref var buil_0 = ref CellBuildEs.Build(idx_0).BuildTC;
                ref var ownBuil_0 = ref CellBuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;

                if (fire_0.Have)
                {
                    Environment(EnvironmentTypes.AdultForest, idx_0)
                        .Resources.Take(CellEnvironmentValues.MaxResources(EnvironmentTypes.AdultForest) / 5);

                    if (unit_0.Have)
                    {
                        CellUnitEs.Hp(idx_0).AmountC.Take(UnitDamageValues.FIRE_DAMAGE);
                        if (!CellUnitEs.Hp(idx_0).AmountC.Have)
                        {
                            CellUnitEs.Kill(idx_0);
                        }
                    }



                    if (!Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                    {
                        CellBuildEs.Remove(idx_0);

                        Remove(EnvironmentTypes.AdultForest, idx_0);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            SetNew(EnvironmentTypes.YoungForest, idx_0);
                        }


                        fire_0.Disable();


                        foreach (var idx_1 in CellSpaceSupport.GetIdxsAround(idx_0))
                        {
                            if (Parent(idx_1).IsActiveSelf.IsActive)
                            {
                                if (Environment(EnvironmentTypes.AdultForest, idx_1).Resources.Have)
                                {
                                    CellFireEs.Fire(idx_1).Fire.Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}