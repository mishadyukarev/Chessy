using Chessy.Common.Extension;
using Chessy.Game.Enum;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void TrySetUnitOnCellM(in UnitTypes unitT, in Player sender, in byte cellIdx)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (_eMG.IsStartedCellC(cellIdx).IsStartedCell(whoseMove) && !_eMG.UnitTC(cellIdx).HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (_eMG.LessonTC.LessonT == LessonTypes.SettingKing)
                    {
                        _eMG.LessonTC.SetNextLesson();
                    }
                }
                else if (unitT.IsGod())
                {
                    if (_eMG.LessonTC.LessonT == LessonTypes.SettingGod)
                    {
                        _eMG.LessonTC.SetNextLesson();
                    }
                }


                SetNewUnitOnCellS(unitT, whoseMove, cellIdx);


                _eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}