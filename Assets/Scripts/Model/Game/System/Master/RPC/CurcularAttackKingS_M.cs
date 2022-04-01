using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System.Master
{
    sealed class CurcularAttackKingS_M : SystemModelGameAbs
    {
        internal CurcularAttackKingS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Attack(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (eMG.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    eMG.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                    eMG.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);


                    foreach (var idxC_0 in eMG.CellEs(cell_0).AroundCellsEs.AroundCellIdxsC)
                    {
                        var idx_1 = idxC_0.Idx;

                        if (eMG.UnitTC(idx_1).HaveUnit)
                        {
                            if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                            {
                                if (eMG.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    sMG.UnitSs.AttackShieldS.Attack(1f, idx_1);
                                }

                                else
                                {
                                    sMG.UnitSs.AttackUnitS.Attack(HpValues.MAX / 4, eMG.UnitPlayerTC(cell_0).PlayerT, idx_1);
                                }
                            }
                        }
                    }

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