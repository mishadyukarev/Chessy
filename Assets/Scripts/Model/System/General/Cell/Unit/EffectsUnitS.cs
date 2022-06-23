using Chessy.Model.Model.Entity.Cell.Unit;

namespace Chessy.Model.Model.System
{
    static class EffectsUnitS
    {
        internal static void Set(this UnitEffectsE unitEffectsE, in float stun, in float protection, in int shoots, in bool haveKingEffect)
        {
            unitEffectsE.StunC.Stun = stun;
            unitEffectsE.ShieldEffectC.Protection = protection;
            unitEffectsE.FrozenArrawC.Shoots = shoots;
            unitEffectsE.HaveKingEffect = haveKingEffect;
        }
        internal static void CopyEffects(this UnitEffectsE unitEffectsToE, in UnitEffectsE unitEffectsFromE)
        {
            unitEffectsToE.StunC = unitEffectsFromE.StunC;
            unitEffectsToE.ShieldEffectC = unitEffectsFromE.ShieldEffectC;
            unitEffectsToE.FrozenArrawC = unitEffectsFromE.FrozenArrawC;
            unitEffectsToE.HaveKingEffect = unitEffectsFromE.HaveKingEffect;
        }
    }
}