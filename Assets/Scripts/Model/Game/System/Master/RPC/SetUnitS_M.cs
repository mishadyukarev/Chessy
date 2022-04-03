using Chessy.Common.Entity;
using Chessy.Common.Extension;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class SetUnitS_M : SystemModelGameAbs
    {
        internal SetUnitS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in Player sender, in byte cell)
        {
            var whoseMove = eMG.WhoseMove.PlayerT;

            if (eMG.CellEs(cell).CellE.IsStartedCell(whoseMove) && !eMG.UnitTC(cell).HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (eMG.LessonTC.LessonT == LessonTypes.SettingKing)
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }
                else if (unitT.IsGod())
                {
                    if (eMG.LessonTC.LessonT == LessonTypes.SettingGod)
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }

                if (eMG.LessonTC.Is(LessonTypes.SettingPawn2))
                {
                    if (unitT.Is(UnitTypes.Pawn))
                    {
                        eMG.LessonTC.SetNextLesson();
                    }
                }


                sMG.UnitSs.SetNewUnitS.Set(unitT, whoseMove, cell);


                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}