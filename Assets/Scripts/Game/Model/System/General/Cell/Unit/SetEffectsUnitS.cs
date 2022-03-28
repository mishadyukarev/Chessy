using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class SetEffectsUnitS
    {
        readonly EffectsE _unitEffectE;

        internal SetEffectsUnitS(in EffectsE unitEffectE) { _unitEffectE = unitEffectE; }

        internal void Set(in float stun, in float protection, in int shoots, in bool haveKingEffect)
        {
            _unitEffectE.StunC.Stun = stun;
            _unitEffectE.ShieldEffectC.Protection = protection;
            _unitEffectE.FrozenArrawC.Shoots = shoots;
            _unitEffectE.HaveKingEffect = haveKingEffect;
        }
        internal void Set(in EffectsE unitEffectsE)
        {
            _unitEffectE.StunC = unitEffectsE.StunC;
            _unitEffectE.ShieldEffectC = unitEffectsE.ShieldEffectC;
            _unitEffectE.FrozenArrawC = unitEffectsE.FrozenArrawC;
            _unitEffectE.HaveKingEffect = unitEffectsE.HaveKingEffect;
        }
    }
}