﻿using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class FirePawnS_M : SystemModel
    {
        internal FirePawnS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Fire(in byte cellIdxForFire, in Player sender)
        {
            if (eMG.StepUnitC(cellIdxForFire).Steps >= StepValues.FIRE_PAWN)
            {
                if (eMG.AdultForestC(cellIdxForFire).HaveAnyResources)
                {
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    eMG.HaveFire(cellIdxForFire) = true;
                    eMG.StepUnitC(cellIdxForFire).Steps -= StepValues.FIRE_PAWN;

                    if(eMG.LessonT == Enum.LessonTypes.PawnFireAdultForest)
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }
                else
                {
                    eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.Mistake);
                }
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}