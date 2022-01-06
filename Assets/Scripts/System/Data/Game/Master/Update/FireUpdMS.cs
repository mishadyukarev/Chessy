using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class FireUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy_0 = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

                ref var hpUnitCell_0 = ref Unit<UnitCellEC>(idx_0);
                ref var hpUnit_0 = ref Unit<HpC>(idx_0);

                ref var buildE_0 = ref Build<BuildCellEC>(idx_0);
                ref var buil_0 = ref Build<BuildC>(idx_0);
                ref var ownBuil_0 = ref Build<OwnerC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

                ref var envE_0 = ref Environment<EnvCellEC>(idx_0);
                ref var envRes_0 = ref Environment<EnvResC>(idx_0);

                ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);


                if (cloud_0.Have)
                {
                    fire_0.Disable();
                }

                if (fire_0.Have)
                {
                    envRes_0.Take(EnvTypes.AdultForest, 2);

                    if (unit_0.Have)
                    {
                        hpUnitCell_0.TakeFire();

                        if (!hpUnit_0.Have)
                        {
                            Unit<UnitCellEC>(idx_0).Kill();
                        }
                    }



                    if (!envRes_0.Have(EnvTypes.AdultForest))
                    {
                        buildE_0.Remove();

                        envE_0.Remove(EnvTypes.AdultForest);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            envE_0.SetNew(EnvTypes.YoungForest);
                        }


                        fire_0.Disable();


                        var aroundXYList = CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = IdxCell(xy1);

                            if (Cell<CellC>(curIdxCell1).IsActiveCell)
                            {
                                if (Environment<EnvironmentC>(curIdxCell1).Have(EnvTypes.AdultForest))
                                {
                                    Fire<HaveEffectC>(curIdxCell1).Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}