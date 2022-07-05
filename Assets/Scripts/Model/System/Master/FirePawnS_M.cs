using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryFireForestWithSimplePawnM(in byte cellIdxForFire, in Player sender)
        {
            if (_e.EnergyUnitC(cellIdxForFire).Energy >= StepValues.FIRE_PAWN)
            {
                if (_e.AdultForestC(cellIdxForFire).HaveAnyResources)
                {
                    _s.RpcSs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    _e.HaveFire(cellIdxForFire) = true;
                    _e.EnergyUnitC(cellIdxForFire).Energy -= StepValues.FIRE_PAWN;

                    if (_e.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                         _s.SetNextLesson();
                    }
                }
                else
                {
                    _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
                }
            }

            else
            {
                _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}