using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class CurcularAttackKingS_M : SystemModel
    {
        internal CurcularAttackKingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Attack(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));
                    eMG.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);


                    foreach (byte idx_1 in eMG.AroundCellsE(cell_0).CellsAround)
                    {
                        if (eMG.UnitTC(idx_1).HaveUnit)
                        {
                            if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                            {
                                if (eMG.ExtraToolWeaponTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    sMG.UnitSs.AttackShieldS.Attack(1f, idx_1);
                                }
                                else if (eMG.ShieldUnitEffectC(idx_1).HaveAnyProtection)
                                {
                                    eMG.ShieldUnitEffectC(idx_1).Protection--;
                                }

                                else
                                {
                                    sMG.UnitSs.AttackUnitS.Attack(HpValues.MAX / 4, eMG.UnitPlayerTC(cell_0).PlayerT, idx_1);
                                }
                            }
                        }
                    }

                    eMG.RpcPoolEs.AnimationCell_ToGeneral(cell_0, AnimationCellTypes.CircularAttackKing, RpcTarget.All);

                    eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;

                    eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AttackMelee);
                }

                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}