using Chessy.Common.Extension;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class SetUnitS_M : SystemModel
    {
        internal SetUnitS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in UnitTypes unitT, in Player sender, in byte cell)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (eMG.IsStartedCellC(cell).IsStartedCell(whoseMove) && !eMG.UnitTC(cell).HaveUnit)
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