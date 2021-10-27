using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FirstButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var needActiveButton = false;

            if (SelectorC.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var selOnUnitCom = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);

                    if (selOnUnitCom.IsMine)
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitUIC.SetActive_Button(BuildButtonTypes.First, needActiveButton);
        }
    }
}
