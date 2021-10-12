using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class RightUnitInfoUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;
        private EcsFilter<CondUnitUICom> _condUnitUIFilt = default;
        private EcsFilter<UniqueAbiltUICom> _uniqueAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);
            ref var condUnitUICom = ref _condUnitUIFilt.Get1(0);
            ref var uniqueAbilUICom = ref _uniqueAbilUIFilt.Get1(0);
            ref var buildAbilUICom = ref _buildAbilUIFilt.Get1(0);


            var needActiveInfoText = false;

            if (selCom.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(selCom.IdxSelCell);


                if (selUnitDatCom.HaveUnit)
                {
                    ref var selOwnUnitCom = ref _cellUnitFilt.Get2(selCom.IdxSelCell);


                    if (selOwnUnitCom.IsMine)
                    {
                        needActiveInfoText = true;
                    }
                }
            }
        }
    }
}
