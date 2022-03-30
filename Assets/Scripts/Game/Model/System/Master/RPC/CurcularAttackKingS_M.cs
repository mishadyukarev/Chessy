using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    sealed class CurcularAttackKingS_M : SystemModelGameAbs
    {
        internal CurcularAttackKingS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Attack(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!e.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (e.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    e.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                    e.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);


                    foreach (var idxC_0 in e.CellEs(cell_0).AroundCellsEs.AroundCellIdxsC)
                    {
                        var idx_1 = idxC_0.Idx;

                        if (e.UnitTC(idx_1).HaveUnit)
                        {
                            if (!e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(cell_0).Player))
                            {
                                if (e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    s.AttackShieldS.Attack(1f, idx_1);
                                }

                                else
                                {
                                    s.AttackUnitS.Attack(HpValues.MAX / 4, e.UnitPlayerTC(cell_0).Player, idx_1);
                                }
                            }
                        }
                    }

                    e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;

                    e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AttackMelee);
                }

                else
                {
                    e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}