using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryFireWithSimplePawnM(in byte cellIdxForFire, in Player sender)
        {
            if (_eMG.StepUnitC(cellIdxForFire).Steps >= StepValues.FIRE_PAWN)
            {
                if (_eMG.AdultForestC(cellIdxForFire).HaveAnyResources)
                {
                    _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    _eMG.HaveFire(cellIdxForFire) = true;
                    _eMG.StepUnitC(cellIdxForFire).Steps -= StepValues.FIRE_PAWN;

                    if (_eMG.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        _eMG.LessonTC.SetNextLesson();
                    }
                }
                else
                {
                    _sMG.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
                }
            }

            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}