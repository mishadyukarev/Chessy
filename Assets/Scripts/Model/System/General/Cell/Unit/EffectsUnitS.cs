using Chessy.Model.Cell.Unit;

namespace Chessy.Model
{
    static class EffectsUnitS
    {
        internal static void Set(this ref EffectsUnitC unitEffectsE, in float stun, in float protection, in int shoots)
        {
            unitEffectsE.StunHowManyUpdatesNeedStay = stun;
            unitEffectsE.ProtectionRainyMagicShield = protection;
            unitEffectsE.ShootsFrozenArrawArcher = shoots;
        }
        internal static void CopyEffects(this ref EffectsUnitC unitEffectsToE, in EffectsUnitC unitEffectsFromE)
        {
            unitEffectsToE.StunHowManyUpdatesNeedStay = unitEffectsFromE.StunHowManyUpdatesNeedStay;
            unitEffectsToE.ProtectionRainyMagicShield = unitEffectsFromE.ProtectionRainyMagicShield;
            unitEffectsToE.ShootsFrozenArrawArcher = unitEffectsFromE.ShootsFrozenArrawArcher;
        }
    }
}