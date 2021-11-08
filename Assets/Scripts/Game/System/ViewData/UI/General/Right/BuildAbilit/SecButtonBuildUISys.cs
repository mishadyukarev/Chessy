using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SecButtonBuildUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, OwnerCom> _cellUnitFilt = default;

        public void Run()
        {
            var needActiveButton = false;

            if (SelectorC.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);

                if (selUnitDatCom.Is(UnitTypes.Pawn))
                {
                    ref var sellOnUnitCom = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);

                    if (sellOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                    {
                        needActiveButton = true;
                    }
                }
            }

            BuildAbilitViewUIC.SetActive_Button(BuildButtonTypes.Second, needActiveButton);
        }
    }
}
