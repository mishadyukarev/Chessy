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
        internal void TryChangeDirectWindWithSnowyM(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (_eMG.StepUnitC(cell_from).Steps >= StepValues.Need(abilityT))
            {
                _eMG.WeatherE.WindC.DirectT = _eMG.AroundCellsE(_eMG.WeatherE.CloudC.Center).Direct(idx_to);
                _eMG.StepUnitC(cell_from).Steps -= StepValues.Need(abilityT);
                _eMG.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);

                if(_eMG.LessonT == LessonTypes.ChangeDirectionWind)
                {
                    _eMG.LessonTC.SetNextLesson();
                }

            }

            else _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}