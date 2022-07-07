using Chessy.Model.Cell.Unit;
namespace Chessy.Model
{
    static class EffectsUnitS
    {
        internal static void Set(this ref EffectsUnitC unitEffectsE, in float stun, in float protection, in bool haveFrozenArrawArcher)
        {
            unitEffectsE.StunHowManyUpdatesNeedStay = stun;
            unitEffectsE.ProtectionRainyMagicShield = protection;
            unitEffectsE.HaveFrozenArrawArcher = haveFrozenArrawArcher;
        }
        internal static void CopyEffects(this ref EffectsUnitC unitEffectsToE, in EffectsUnitC unitEffectsFromE)
        {
            unitEffectsToE.StunHowManyUpdatesNeedStay = unitEffectsFromE.StunHowManyUpdatesNeedStay;
            unitEffectsToE.ProtectionRainyMagicShield = unitEffectsFromE.ProtectionRainyMagicShield;
            unitEffectsToE.HaveFrozenArrawArcher = unitEffectsFromE.HaveFrozenArrawArcher;
        }
    }
}