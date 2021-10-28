using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        private EcsFilter<SelectorC> _selComFilter = default;

        private EcsFilter<CellUnitMainViewCom, CellUnitExtraViewComp> _cellUnitViewFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);

                ref var curMainUnitViewCom = ref _cellUnitViewFilter.Get1(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitViewFilter.Get2(idxCurCell);


                if (SelectorC.IdxSelCell == idxCurCell)
                {
                    if (curUnitDatCom.HaveUnit)
                    {
                        if (curOwnUnitCom.IsMine)
                        {
                            if (curUnitDatCom.Is(UnitTypes.Rook))
                            {
                                curMainUnitViewCom.Set_LocRotEuler(new Vector3(0, 0, -90));
                            }
                            else
                            {
                                curMainUnitViewCom.SetFlipX(true);
                                curExtraUnitViewCom.SetFlipX(true);
                            }
                        }
                    }
                }

                else
                {
                    curMainUnitViewCom.Set_LocRotEuler(new Vector3(0, 0, 0));

                    curMainUnitViewCom.SetFlipX(false);
                    curExtraUnitViewCom.SetFlipX(false);
                }
            }
        }
    }
}
