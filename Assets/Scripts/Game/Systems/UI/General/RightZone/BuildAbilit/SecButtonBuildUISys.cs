using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SecButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            ref var selCom = ref _selFilt.Get1(0);
            ref var buildAbilUICom = ref _buildAbilUIFilt.Get1(0);


            var needActiveButton = false;

            if (selCom.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(selCom.IdxSelCell);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref _cellUnitFilt.Get2(selCom.IdxSelCell);

                    if (sellOnUnitCom.IsMine)
                    {
                        needActiveButton = true;
                    }
                }
            }

            buildAbilUICom.SetActive_Button(BuildButtonTypes.Second, needActiveButton);
        }
    }
}
