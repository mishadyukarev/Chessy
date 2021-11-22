using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class FireUpdMasSys : IEcsRunSystem
    {
        private readonly EcsFilter<XyC> _xyF = default;
        private readonly EcsFilter<CellC> _cellF = default;

        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private readonly EcsFilter<HpC> _statUnitF = default;

        private readonly EcsFilter<FireC> _fireF = default;
        private readonly EcsFilter<EnvC, EnvResC> _envF = default;
        private readonly EcsFilter<BuildC, OwnerC> _buildF = default;
        private readonly EcsFilter<CloudC> _cloudsF = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyF)
            {
                var curXy = _xyF.Get1(idx_0).Xy;

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);

                ref var buil_0 = ref _buildF.Get1(idx_0);
                ref var ownBuil_0 = ref _buildF.Get2(idx_0);

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
                    envRes_0.TakeAmountRes(EnvTypes.AdultForest, 2);

                    if (unit_0.HaveUnit)
                    {
                        hpUnit_0.TakeHp(40);

                        if (!hpUnit_0.HaveHp)
                        {
                            unit_0.Kill(levUnit_0.Level, ownUnit_0.Owner);
                        }
                    }



                    if (!envRes_0.HaveRes(EnvTypes.AdultForest))
                    {
                        if (buil_0.Have)
                        {
                            buil_0.Remove(ownBuil_0.Owner);
                        }

                        env_0.Remove(EnvTypes.AdultForest);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            ref var envDatCom = ref _envF.Get1(idx_0);

                            envDatCom.SetNew(EnvTypes.YoungForest);
                        }


                        fire_0.Disable();


                        var aroundXYList = CellSpace.GetXyAround(_xyF.Get1(idx_0).Xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = _xyF.GetIdxCell(xy1);

                            if (_cellF.Get1(curIdxCell1).IsActiveCell)
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