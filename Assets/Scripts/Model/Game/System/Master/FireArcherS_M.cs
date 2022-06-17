using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {

        internal void FirePawn(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (eMG.UnitForArsonC(cell_from).Contains(cell_to))
            {
                if (eMG.StepUnitC(cell_from).Steps >= StepValues.ARCHER_FIRE)
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    eMG.StepUnitC(cell_from).Steps -= StepValues.ARCHER_FIRE;
                    eMG.HaveFire(cell_to) = true;

                    if (eMG.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }

                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}