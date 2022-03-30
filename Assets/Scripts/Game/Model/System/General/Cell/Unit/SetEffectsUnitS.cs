using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SetEffectsUnitS : SystemModelGameAbs
    {
        internal SetEffectsUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in float stun, in float protection, in int shoots, in bool haveKingEffect, in byte cell_0)
        {
            e.UnitEffectStunC(cell_0).Stun = stun;
            e.UnitEffectShield(cell_0).Protection = protection;
            e.UnitEffectFrozenArrawC(cell_0).Shoots = shoots;
            e.UnitEffectsE(cell_0).HaveKingEffect = haveKingEffect;
        }

        internal void Set(in byte cell_from, in byte cell_to) => e.UnitEffectsE(cell_to) = e.UnitEffectsE(cell_from);
    }
}