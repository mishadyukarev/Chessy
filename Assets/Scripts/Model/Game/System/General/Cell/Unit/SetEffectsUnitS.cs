using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetEffectsUnitS : SystemModel
    {
        internal SetEffectsUnitS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in float stun, in float protection, in int shoots, in bool haveKingEffect, in byte cell_0)
        {
            eMG.StunUnitC(cell_0).Stun = stun;
            eMG.ShieldUnitEffectC(cell_0).Protection = protection;
            eMG.FrozenArrawEffectC(cell_0).Shoots = shoots;
            eMG.HaveKingEffect(cell_0) = haveKingEffect;
        }

        internal void Set(in byte cell_from, in byte cell_to)
        {
            eMG.StunUnitC(cell_to) = eMG.StunUnitC(cell_from);
            eMG.ShieldUnitEffectC(cell_to) = eMG.ShieldUnitEffectC(cell_from);
            eMG.FrozenArrawEffectC(cell_to) = eMG.FrozenArrawEffectC(cell_from);
            eMG.HaveKingEffect(cell_to) = eMG.HaveKingEffect(cell_from);
        }
    }
}