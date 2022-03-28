using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    sealed class CurcularAttackKingS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;
        readonly SystemsModelGame _systems;

        internal CurcularAttackKingS_M(in CellEs cellEs, in SystemsModelGame systems, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
            _systems = systems;
        }

        internal void Attack(in AbilityTypes abilityT, in Player sender)
        {
            if (!_cellEs.UnitEs.CoolDownC(abilityT).HaveCooldown)
            {
                if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.Need(abilityT))
                {
                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    _cellEs.UnitEs.CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);
                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.Need(abilityT);


                    foreach (var idxC_0 in _cellEs.AroundCellsEs.AroundCellIdxsC)
                    {
                        var idx_1 = idxC_0.Idx;

                        if (e.UnitTC(idx_1).HaveUnit)
                        {
                            if (!e.UnitPlayerTC(idx_1).Is(_cellEs.UnitMainE.PlayerTC.Player))
                            {
                                if (e.UnitExtraTWTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    _systems.CellSs(idx_1).AttackShieldS.Attack(1f);
                                }

                                else
                                {
                                    _systems.CellSs(idx_1).AttackUnitS.Attack(HpValues.MAX / 4, _cellEs.UnitMainE.PlayerTC.Player);
                                }
                            }
                        }
                    }

                    _cellEs.UnitMainE.ConditionTC.Condition = ConditionUnitTypes.None;

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