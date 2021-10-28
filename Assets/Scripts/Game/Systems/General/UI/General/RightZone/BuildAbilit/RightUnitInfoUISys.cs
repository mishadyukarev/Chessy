using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class RightUnitInfoUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var needActiveInfoText = false;

            if (SelectorC.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);


                if (selUnitDatCom.HaveUnit)
                {
                    ref var selOwnUnitCom = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);


                    if (selOwnUnitCom.IsMine)
                    {
                        needActiveInfoText = true;
                    }
                }
            }
        }
    }
}
