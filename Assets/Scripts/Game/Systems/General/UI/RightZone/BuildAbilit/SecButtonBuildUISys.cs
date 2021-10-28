using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SecButtonBuildUISys : IEcsRunSystem
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
                    ref var sellOnUnitCom = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);

                    if (sellOnUnitCom.IsMine)
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.Second, needActiveButton);
        }
    }
}
