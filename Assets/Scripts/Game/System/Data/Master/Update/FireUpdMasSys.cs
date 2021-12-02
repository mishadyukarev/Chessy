﻿using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class FireUpdMasSys : IEcsRunSystem
    {
        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private readonly EcsFilter<HpC> _statUnitF = default;

        private readonly EcsFilter<FireC> _fireF = default;
        private readonly EcsFilter<EnvC, EnvResC> _envF = default;
        private readonly EcsFilter<CloudC> _cloudsF = default;

        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                var curXy = EntityPool.Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);

                ref var buil_0 = ref EntityPool.Build<BuildC>(idx_0);
                ref var ownBuil_0 = ref EntityPool.Build<OwnerC>(idx_0);

                ref var fire_0 = ref _fireF.Get1(idx_0);
                ref var env_0 = ref _envF.Get1(idx_0);
                ref var envRes_0 = ref _envF.Get2(idx_0);

                ref var cloud_0 = ref _cloudsF.Get1(idx_0);


                if (cloud_0.Have)
                {
                    fire_0.Disable();
                }

                if (fire_0.Have)
                {
                    envRes_0.Take(EnvTypes.AdultForest, 2);

                    if (unit_0.Have)
                    {
                        hpUnit_0.Take(40);

                        if (!hpUnit_0.HaveHp)
                        {
                            unit_0.Kill(levUnit_0.Level, ownUnit_0.Owner);
                        }
                    }



                    if (!envRes_0.Have(EnvTypes.AdultForest))
                    {
                        buil_0.Remove();

                        env_0.Remove(EnvTypes.AdultForest);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            ref var envDatCom = ref _envF.Get1(idx_0);

                            envDatCom.SetNew(EnvTypes.YoungForest);
                        }


                        fire_0.Disable();


                        var aroundXYList = CellSpaceC.XyAround(EntityPool.Cell<XyC>(idx_0).Xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = EntityPool.IdxCell(xy1);

                            if (EntityPool.Cell<CellC>(curIdxCell1).IsActiveCell)
                            {
                                if (_envF.Get1(curIdxCell1).Have(EnvTypes.AdultForest))
                                {
                                    _fireF.Get1(curIdxCell1).Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}