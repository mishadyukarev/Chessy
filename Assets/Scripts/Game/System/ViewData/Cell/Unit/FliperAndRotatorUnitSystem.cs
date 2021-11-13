using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        private EcsFilter<UnitMainVC> _cellUnitViewFilter = default;
        private EcsFilter<UnitC, OwnerC> _cellUnitFilter = default;

        private EcsFilter<CornerArcherC> _archerFilt = default;

        private EcsFilter<UnitExtraVC> _unitExtVF = default;

        public void Run()
        {
            foreach (byte idx_0 in _cellUnitFilter)
            {
                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var ownUnit_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var corner_0 = ref _archerFilt.Get1(idx_0);

                ref var main_0 = ref _cellUnitViewFilter.Get1(idx_0);
                ref var extra_0 = ref _unitExtVF.Get1(idx_0);


                if (SelectorC.IdxSelCell == idx_0)
                {
                    if (unit_0.HaveUnit)
                    {
                        if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                if (corner_0.IsCornered)
                                {
                                    main_0.Set_LocRotEuler(new Vector3(0, 0, -90));
                                    main_0.SetFlipX(false);
                                }
                                else
                                {
                                    main_0.Set_LocRotEuler(new Vector3(0, 0, 0));
                                    main_0.SetFlipX(true);
                                }
                            }
                            else
                            {
                                main_0.SetFlipX(true);
                                extra_0.SetFlipX(true);
                            }
                        }
                    }
                }

                else
                {
                    main_0.Set_LocRotEuler(new Vector3(0, 0, 0));

                    main_0.SetFlipX(false);
                    extra_0.SetFlipX(false);
                }
            }
        }
    }
}
