using Chessy.Model.Enum;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TrySetUnitOnCellM(in UnitTypes unitT, in Player sender, in byte cellIdx)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if (_isStartedCellCs[cellIdx].IsStartedCell(whoDoing) && !_unitCs[cellIdx].HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (_aboutGameC.LessonT == LessonTypes.SettingKing)
                    {
                         SetNextLesson();
                    }
                }
                else if (unitT.IsGod())
                {
                    if (_aboutGameC.LessonT == LessonTypes.SettingGod)
                    {
                         SetNextLesson();
                    }
                }


                SetNewUnitOnCellS.Set(unitT, whoDoing, cellIdx);


                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}