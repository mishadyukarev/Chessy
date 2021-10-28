using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class RightUnitInfoUISys : IEcsRunSystem
    {
        private EcsFilter<BuildAbilitUIC> _buildAbilUIFilt = default;
        private EcsFilter<CondUnitUIC> _condUnitUIFilt = default;
        private EcsFilter<RightUniqueViewUIC> _uniqueAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var condUnitUICom = ref _condUnitUIFilt.Get1(0);
            ref var uniqueAbilUICom = ref _uniqueAbilUIFilt.Get1(0);
            ref var buildAbilUICom = ref _buildAbilUIFilt.Get1(0);


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
