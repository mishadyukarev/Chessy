using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class EffectsUISys : IEcsRunSystem
    {
        private EcsFilter<UnitC> _unitF = default;
        private EcsFilter<EffectsC> _effUnitF = default;

        public void Run()
        {
            ref var unitC_sel = ref _unitF.Get1(SelIdx<IdxC>().Idx);
            ref var effUnitC_sel = ref _effUnitF.Get1(SelIdx<IdxC>().Idx);

            //EffectsIUC.SetColor(UnitStatTypes.Hp, effUnitC_sel.Have(UnitStatTypes.Hp));
            EffectsUIC.SetColor(UnitStatTypes.Damage, effUnitC_sel.Have(UnitStatTypes.Damage));
            EffectsUIC.SetColor(UnitStatTypes.Steps, effUnitC_sel.Have(UnitStatTypes.Steps));
        }
    }
}