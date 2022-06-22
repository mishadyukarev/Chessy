using Chessy.Game.Enum;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void TrySetUnitOnCellM(in UnitTypes unitT, in Player sender, in byte cellIdx)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            if (_e.IsStartedCellC(cellIdx).IsStartedCell(whoseMove) && !_e.UnitT(cellIdx).HaveUnit())
            {
                if (unitT == UnitTypes.King)
                {
                    if (_e.LessonT == LessonTypes.SettingKing)
                    {
                        _e.LessonT.SetNextLesson();
                    }
                }
                else if (unitT.IsGod())
                {
                    if (_e.LessonT == LessonTypes.SettingGod)
                    {
                        _e.LessonT.SetNextLesson();
                    }
                }


                SetNewUnitOnCellS(unitT, whoseMove, cellIdx);


                ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}