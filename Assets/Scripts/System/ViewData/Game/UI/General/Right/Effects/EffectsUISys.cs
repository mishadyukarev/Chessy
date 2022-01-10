using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    sealed class EffectsUISys : IEcsRunSystem
    {
        public void Run()
        {
            EffectsUIC.SetColor(UnitStatTypes.Damage, Unit<HaveEffectC>(UnitStatTypes.Damage, SelIdx<IdxC>().Idx).Have);
            EffectsUIC.SetColor(UnitStatTypes.Steps, Unit<HaveEffectC>(UnitStatTypes.Steps, SelIdx<IdxC>().Idx).Have);
        }
    }
}