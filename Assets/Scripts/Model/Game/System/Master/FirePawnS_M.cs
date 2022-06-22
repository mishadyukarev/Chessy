﻿using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryFireForestWithSimplePawnM(in byte cellIdxForFire, in Player sender)
        {
            if (_e.StepUnitC(cellIdxForFire).Steps >= StepValues.FIRE_PAWN)
            {
                if (_e.AdultForestC(cellIdxForFire).HaveAnyResources)
                {
                    _s.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    _e.HaveFire(cellIdxForFire) = true;
                    _e.StepUnitC(cellIdxForFire).Steps -= StepValues.FIRE_PAWN;

                    if (_e.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        _e.LessonT.SetNextLesson();
                    }
                }
                else
                {
                    _s.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
                }
            }

            else
            {
                _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}