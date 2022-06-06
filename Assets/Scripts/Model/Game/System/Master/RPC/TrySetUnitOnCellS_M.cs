using Chessy.Common.Extension;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class TrySetUnitOnCellS_M : SystemModel
    {
        internal TrySetUnitOnCellS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void TrySet(in UnitTypes unitT, in Player sender, in byte cellIdx)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (eMG.IsStartedCellC(cellIdx).IsStartedCell(whoseMove) && !eMG.UnitTC(cellIdx).HaveUnit)
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


                sMG.UnitSs.SetNewOnCellS.Set(unitT, whoseMove, cellIdx);


                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}