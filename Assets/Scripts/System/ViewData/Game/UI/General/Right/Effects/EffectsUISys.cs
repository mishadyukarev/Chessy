using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    sealed class EffectsUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var unitC_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
            ref var effUnitC_sel = ref Unit<EffectsC>(SelIdx<IdxC>().Idx);

            //EffectsIUC.SetColor(UnitStatTypes.Hp, effUnitC_sel.Have(UnitStatTypes.Hp));
            EffectsUIC.SetColor(UnitStatTypes.Damage, effUnitC_sel.Have(UnitStatTypes.Damage));
            EffectsUIC.SetColor(UnitStatTypes.Steps, effUnitC_sel.Have(UnitStatTypes.Steps));
        }
    }
}