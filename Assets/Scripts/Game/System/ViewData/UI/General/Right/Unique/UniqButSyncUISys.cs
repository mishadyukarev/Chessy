using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class UniqButSyncUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, UniqAbilC> _unitAbilFilt = default;

        public void Run()
        {
            ref var uniq_sel = ref _unitAbilFilt.Get2(SelectorC.IdxSelCell);

            UniqButtonsViewC.SetActive(UniqButtonTypes.First, uniq_sel.Ability(UniqButtonTypes.First));
            UniqButtonsViewC.SetActive(UniqButtonTypes.Second, uniq_sel.Ability(UniqButtonTypes.Second));
            UniqButtonsViewC.SetActive(UniqButtonTypes.Third, default);
        }
    }
}