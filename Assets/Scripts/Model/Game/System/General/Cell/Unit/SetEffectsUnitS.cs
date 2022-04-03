using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetEffectsUnitS : SystemModelGameAbs
    {
        internal SetEffectsUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in float stun, in float protection, in int shoots, in bool haveKingEffect, in byte cell_0)
        {
            eMG.UnitEffectStunC(cell_0).Stun = stun;
            eMG.UnitEffectShield(cell_0).Protection = protection;
            eMG.UnitEffectFrozenArrawC(cell_0).Shoots = shoots;
            eMG.UnitEffectsE(cell_0).HaveKingEffect = haveKingEffect;
        }

        internal void Set(in byte cell_from, in byte cell_to) => eMG.UnitEffectsE(cell_to) = eMG.UnitEffectsE(cell_from);
    }
}