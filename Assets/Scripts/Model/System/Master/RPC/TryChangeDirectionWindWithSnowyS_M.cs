using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryChangeDirectWindWithSnowyM(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            //if (_e.EnergyUnitC(cell_from).Energy >= StepValues.Need(abilityT))
            //{
                _e.DirectWindT = _e.AroundCellsE(_e.CenterCloudCellIdx).Direct(idx_to);
                //_e.EnergyUnitC(cell_from).Energy -= StepValues.Need(abilityT);
                _e.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);

                if (_e.LessonT == LessonTypes.ChangeDirectionWind)
                {
                     _s.SetNextLesson();
                }

            //}

            //else _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}