using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class EffectsUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, UnitEffectsC> _cellUnitFilt = default;

        public void Run()
        {
            ref var unitC_sel = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);
            ref var effUnitC_sel = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);

            //EffectsIUC.SetColor(UnitStatTypes.Hp, effUnitC_sel.Have(UnitStatTypes.Hp));
            EffectsIUC.SetColor(UnitStatTypes.Damage, effUnitC_sel.Have(UnitStatTypes.Damage));
            EffectsIUC.SetColor(UnitStatTypes.Steps, effUnitC_sel.Have(UnitStatTypes.Steps));
        }
    }
}