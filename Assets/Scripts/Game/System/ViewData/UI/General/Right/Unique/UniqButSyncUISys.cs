using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class UniqButSyncUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, Uniq1C, Uniq2C> _unitAbilFilt = default;

        public void Run()
        {
            ref var uniq1_sel = ref _unitAbilFilt.Get2(SelectorC.IdxSelCell);
            ref var uniq2_sel = ref _unitAbilFilt.Get3(SelectorC.IdxSelCell);

            UniqButtonsViewC.SetActive(UniqButtonTypes.First, uniq1_sel.Ability);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Second, uniq2_sel.Ability);
            UniqButtonsViewC.SetActive(UniqButtonTypes.Third, default);
        }
    }
}