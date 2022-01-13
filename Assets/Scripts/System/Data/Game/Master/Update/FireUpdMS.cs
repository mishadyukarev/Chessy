using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellFirePool;
using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    struct FireUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy_0 = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerC>(idx_0);

                ref var hpUnitCell_0 = ref Unit<UnitCellEC>(idx_0);
                ref var hpUnit_0 = ref Unit<HpC>(idx_0);

                ref var buildE_0 = ref Build<BuildCellEC>(idx_0);
                ref var buil_0 = ref Build<BuildingC>(idx_0);
                ref var ownBuil_0 = ref Build<PlayerC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

                ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);


                if (cloud_0.Have)
                {
                    fire_0.Disable();
                }

                if (fire_0.Have)
                {
                    Environment<AmountResourcesC>(EnvTypes.AdultForest, idx_0).Resources -= 2;

                    if (unit_0.Have)
                    {
                        hpUnitCell_0.TakeFire();

                        if (!hpUnit_0.Have)
                        {
                            Unit<UnitCellEC>(idx_0).Kill();
                        }
                    }



                    if (!Environment<AmountResourcesC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        buildE_0.Remove();

                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).Remove();


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).SetNew();
                        }


                        fire_0.Disable();


                        var aroundXYList = CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = IdxCell(xy1);

                            if (Cell<IsActivatedC>(curIdxCell1).IsActivated)
                            {
                                if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, curIdxCell1).Have)
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