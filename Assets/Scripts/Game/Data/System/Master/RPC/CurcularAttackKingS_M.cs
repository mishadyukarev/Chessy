using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public static class CurcularAttackKingS_M
    {
        public static void CurcularAttack(in byte idx_0, in AbilityTypes abilityT, in Player sender, in EntitiesModel e)
        {
            if (!e.UnitEs(idx_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (e.UnitStepC(idx_0).Steps >= StepValues.Need(abilityT))
                {
                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    e.UnitEs(idx_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                    e.UnitStepC(idx_0).Steps -= StepValues.Need(abilityT);


                    foreach (var idxC_0 in e.CellEs(idx_0).AroundCellIdxsC)
                    {
                        var idx_1 = idxC_0.Idx;

                        if (e.UnitTC(idx_1).HaveUnit)
                        {
                            if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_0).Player))
                            {
                                if (e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    new AttackShieldS(1f, idx_1, e);
                                }

                                else
                                {
                                    AttackUnitS.AttackUnit(HpValues.MAX / 4, e.UnitPlayerTC(idx_0).Player, idx_1, e);
                                }
                            }
                        }
                    }

                    e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                    e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AttackMelee);
                }

                else
                {
                    e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}