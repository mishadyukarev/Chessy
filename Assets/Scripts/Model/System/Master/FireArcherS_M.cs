using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {

        internal void TryFireForestWithArcherM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_e.WhereUnitCanFireAdultForestC(cell_from).Can(cell_to))
            {
                if (_e.EnergyUnitC(cell_from).Energy >= StepValues.ARCHER_FIRE)
                {
                    _s.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    _e.EnergyUnitC(cell_from).Energy -= StepValues.ARCHER_FIRE;
                    _e.HaveFire(cell_to) = true;

                    if (_e.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        _e.CommonInfoAboutGameC.SetNextLesson();
                    }
                }

                else
                {
                    _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}