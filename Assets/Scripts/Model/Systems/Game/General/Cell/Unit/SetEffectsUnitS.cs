using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct SetEffectsUnitS
    {
        readonly UnitEffectsE _effectsE;

        internal SetEffectsUnitS(in UnitEffectsE effectsE)
        {
            _effectsE = effectsE;
        }
        internal void Set(in float stun, in float protection, in int shoots, in bool haveKingEffect)
        {
            _effectsE.StunC.Stun = stun;
            _effectsE.ShieldEffectC.Protection = protection;
            _effectsE.FrozenArrawC.Shoots = shoots;
            _effectsE.HaveKingEffect = haveKingEffect;
        }
    }
}