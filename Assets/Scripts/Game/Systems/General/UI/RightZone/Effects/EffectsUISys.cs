using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class EffectsUISys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, UnitEffectsC> _cellUnitFilt = default;

        public void Run()
        {
            ref var unitC_sel = ref _cellUnitFilt.Get1(SelectorC.IdxSelCell);
            ref var effUnitC_sel = ref _cellUnitFilt.Get2(SelectorC.IdxSelCell);

            EffectsIUC.SetColor(StatTypes.Health, effUnitC_sel.Have(StatTypes.Health));
            EffectsIUC.SetColor(StatTypes.Damage, effUnitC_sel.Have(StatTypes.Damage));
            EffectsIUC.SetColor(StatTypes.Steps, effUnitC_sel.Have(StatTypes.Steps));
        }
    }
}