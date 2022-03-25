using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    sealed class CurcularAttackKingS_M : SystemModelGameAbs
    {
        readonly AttackUnitS _attackUnitS;
        readonly AttackShieldS _attackShieldS;

        internal CurcularAttackKingS_M(in AttackUnitS attackUnitS, in AttackShieldS attackShieldS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _attackUnitS = attackUnitS;
            _attackShieldS = attackShieldS;
        }

        public void Attack(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMGame.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (eMGame.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    eMGame.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                    eMGame.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);


                    foreach (var idxC_0 in eMGame.CellEs(cell_0).AroundCellIdxsC)
                    {
                        var idx_1 = idxC_0.Idx;

                        if (eMGame.UnitTC(idx_1).HaveUnit)
                        {
                            if (!eMGame.UnitPlayerTC(idx_1).Is(eMGame.UnitPlayerTC(cell_0).Player))
                            {
                                if (eMGame.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    _attackShieldS.Attack(1f, idx_1);
                                }

                                else
                                {
                                    _attackUnitS.Attack(HpValues.MAX / 4, eMGame.UnitPlayerTC(cell_0).Player, idx_1);
                                }
                            }
                        }
                    }

                    eMGame.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;

                    eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.AttackMelee);
                }

                else
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}