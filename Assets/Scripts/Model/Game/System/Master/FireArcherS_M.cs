using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {

        internal void TryFireForestWithArcherM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_e.UnitForArsonC(cell_from).Contains(cell_to))
            {
                if (_e.StepUnitC(cell_from).Steps >= StepValues.ARCHER_FIRE)
                {
                    _s.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    _e.StepUnitC(cell_from).Steps -= StepValues.ARCHER_FIRE;
                    _e.HaveFire(cell_to) = true;

                    if (_e.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        _e.LessonT.SetNextLesson();
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