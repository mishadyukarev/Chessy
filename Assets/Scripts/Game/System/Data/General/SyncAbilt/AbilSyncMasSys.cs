using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class AbilSyncMasSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, OwnerCom> _unitBaseFilt = default;
        private EcsFilter<CellUnitDataC, Uniq1C, Uniq2C> _unitUniqFilt = default;
        private EcsFilter<CellEnvDataC> _envFilt = default;
        private EcsFilter<CellFireDataC> _fireFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _unitUniqFilt)
            {
                ref var unit_0 = ref _unitUniqFilt.Get1(idx_0);

                ref var ownUnit_0 = ref _unitBaseFilt.Get2(idx_0);

                ref var uniq1_0 = ref _unitUniqFilt.Get2(idx_0);
                ref var uniq2_0 = ref _unitUniqFilt.Get3(idx_0);

                ref var env_0 = ref _envFilt.Get1(idx_0);
                ref var fire_0 = ref _fireFilt.Get1(idx_0);

                if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.HaveUnit)
                    {
                        uniq1_0.SetAbility(unit_0.Unit, env_0, fire_0);
                        uniq2_0.SetAbility(unit_0.Unit);
                    }
                }
                else
                {
                    uniq1_0.Reset();
                    uniq2_0.Reset();
                }
            }
        }
    }
}