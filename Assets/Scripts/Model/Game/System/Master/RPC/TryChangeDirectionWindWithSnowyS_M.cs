using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryChange(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (eMG.StepUnitC(cell_from).Steps >= StepValues.Need(abilityT))
            {
                eMG.WeatherE.WindC.DirectT = eMG.AroundCellsE(eMG.WeatherE.CloudC.Center).Direct(idx_to);
                eMG.StepUnitC(cell_from).Steps -= StepValues.Need(abilityT);
                eMG.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);

                if(eMG.LessonT == LessonTypes.ChangeDirectionWind)
                {
                    eMG.LessonTC.SetNextLesson();
                }

            }

            else eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}