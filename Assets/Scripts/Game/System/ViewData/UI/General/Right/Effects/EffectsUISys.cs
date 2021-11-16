using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class EffectsUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC> _unitF = default;
        private EcsFilter<UnitEffectsC> _effUnitF = default;

        public void Run()
        {
            ref var unitC_sel = ref _unitF.Get1(SelIdx.Idx);
            ref var effUnitC_sel = ref _effUnitF.Get1(SelIdx.Idx);

            //EffectsIUC.SetColor(UnitStatTypes.Hp, effUnitC_sel.Have(UnitStatTypes.Hp));
            EffectsIUC.SetColor(UnitStatTypes.Damage, effUnitC_sel.Have(UnitStatTypes.Damage));
            EffectsIUC.SetColor(UnitStatTypes.Steps, effUnitC_sel.Have(UnitStatTypes.Steps));
        }
    }
}