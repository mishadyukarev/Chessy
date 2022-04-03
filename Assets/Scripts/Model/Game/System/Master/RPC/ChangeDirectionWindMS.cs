using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class ChangeDirectionWindMS : SystemModelGameAbs
    {
        internal ChangeDirectionWindMS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Change(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (eMG.UnitStepC(cell_from).Steps >= StepValues.Need(abilityT))
            {
                eMG.WeatherE.WindC.DirectT = eMG.CellEs(eMG.WeatherE.CloudC.Center).AroundCellsEs.Direct(idx_to);
                eMG.UnitStepC(cell_from).Steps -= StepValues.Need(abilityT);
                eMG.UnitAbilityE(cell_from).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);

                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);
            }

            else eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}