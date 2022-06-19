using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {

        internal void TryFireForestWithPawnM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_eMG.UnitForArsonC(cell_from).Contains(cell_to))
            {
                if (_eMG.StepUnitC(cell_from).Steps >= StepValues.ARCHER_FIRE)
                {
                    _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    _eMG.StepUnitC(cell_from).Steps -= StepValues.ARCHER_FIRE;
                    _eMG.HaveFire(cell_to) = true;

                    if (_eMG.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        _eMG.LessonTC.SetNextLesson();
                    }
                }

                else
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}