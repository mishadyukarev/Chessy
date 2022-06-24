using Chessy.Model.Model.Entity.Cell.Unit;

namespace Chessy.Model.Model.System
{
    static class EffectsUnitS
    {
        internal static void Set(this ref EffectsUnitC unitEffectsE, in float stun, in float protection, in int shoots, in bool haveKingEffect)
        {
            unitEffectsE.StunHowManyUpdatesNeedStay = stun;
            unitEffectsE.ProtectionRainyMagicShield = protection;
            unitEffectsE.ShootsFrozenArrawArcher = shoots;
            unitEffectsE.HaveKingEffect = haveKingEffect;
        }
        internal static void CopyEffects(this ref EffectsUnitC unitEffectsToE, in EffectsUnitC unitEffectsFromE)
        {
            unitEffectsToE.StunHowManyUpdatesNeedStay = unitEffectsFromE.StunHowManyUpdatesNeedStay;
            unitEffectsToE.ProtectionRainyMagicShield = unitEffectsFromE.ProtectionRainyMagicShield;
            unitEffectsToE.ShootsFrozenArrawArcher = unitEffectsFromE.ShootsFrozenArrawArcher;
            unitEffectsToE.HaveKingEffect = unitEffectsFromE.HaveKingEffect;
        }
    }
}