using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class StunElfemaleS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public StunElfemaleS_M(in CellEs cellEs,  in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void Stun(in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!_cellEs.UnitEs.CoolDownC(abilityT).HaveCooldown)
            {
                if (e.AdultForestC(idx_to).HaveAnyResources)
                {
                    if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.STUN_ELFEMALE)
                    {
                        if (!_cellEs.UnitMainE.PlayerTC.Is(e.UnitPlayerTC(idx_to).Player))
                        {
                            e.UnitEffectStunC(idx_to).Stun = StunValues.ELFEMALE;
                            _cellEs.UnitEs.CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.STUN_ELFEMALE;

                            e.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in e.CellEs(idx_to).AroundCellsEs.IdxsAround)
                            {
                                if (e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (e.UnitTC(idx_1).HaveUnit && e.UnitPlayerTC(idx_1).Is(e.UnitPlayerTC(idx_to).Player))
                                    {
                                        e.UnitEffectStunC(idx_1).Stun = StunValues.ELFEMALE;
                                    }
                                }
                            }
                        }
                    }

                    else e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}