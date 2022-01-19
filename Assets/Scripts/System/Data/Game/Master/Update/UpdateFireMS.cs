﻿using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    struct UpdateFireMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy_0 = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

                ref var hpUnit_0 = ref CellUnitHpEs.Hp<AmountC>(idx_0);

                ref var buil_0 = ref Build<BuildingTC>(idx_0);
                ref var ownBuil_0 = ref Build<PlayerTC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

                ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);


                if (cloud_0.Have)
                {
                    fire_0.Disable();
                }

                if (fire_0.Have)
                {
                    

                    Resources(EnvironmentTypes.AdultForest, idx_0)
                        .Take(EnvironmentValues.MaxResources(EnvironmentTypes.AdultForest) / 5);

                    if (unit_0.Have)
                    {
                        CellUnitHpEs.TakeFire(idx_0);
                        if (!CellUnitHpEs.Hp<AmountC>(idx_0).Have)
                        {
                            CellUnitEs.Kill(idx_0);
                        }
                    }



                    if (!Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                    {
                        CellBuildE.Remove(idx_0);

                        Remove(EnvironmentTypes.AdultForest, idx_0);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            SetNew(EnvironmentTypes.YoungForest, idx_0);
                        }


                        fire_0.Disable();


                        foreach (var idx_1 in CellSpaceC.IdxAround(idx_0))
                        {
                            if (CellParent<IsActiveC>(idx_1).IsActive)
                            {
                                if (Resources(EnvironmentTypes.AdultForest, idx_1).Have)
                                {
                                    Fire<HaveEffectC>(idx_1).Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}