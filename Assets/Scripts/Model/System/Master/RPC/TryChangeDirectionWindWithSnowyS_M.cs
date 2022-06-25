using Chessy.Model.Enum;
using Chessy.Model.Values.Cell.Unit;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryChangeDirectWindWithSnowyM(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (_e.EnergyUnitC(cell_from).Energy >= StepValues.Need(abilityT))
            {
                _e.DirectWindT = _e.AroundCellsE(_e.CenterCloudCellIdx).Direct(idx_to);
                _e.EnergyUnitC(cell_from).Energy -= StepValues.Need(abilityT);
                _e.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                _s.SoundToGeneral(RpcTarget.All, abilityT);

                if (_e.LessonT == LessonTypes.ChangeDirectionWind)
                {
                    _e.CommonInfoAboutGameC.SetNextLesson();
                }

            }

            else _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}