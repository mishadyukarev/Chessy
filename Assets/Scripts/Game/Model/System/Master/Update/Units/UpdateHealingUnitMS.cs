using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class UpdateHealingUnitMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateHealingUnitMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                {
                    E.UnitHpC(idx_0).Health = HpValues.MAX;
                }
            }
        }
    }
}