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

            if (_isStartedCellCs[cellIdx].IsStartedCellForPlayer(whoDoing) && !_unitCs[cellIdx].HaveUnit)
            {
                if (unitT == UnitTypes.King)
                {
                    if (AboutGameC.LessonT == LessonTypes.SettingKing)
                    {
                        SetNextLesson();
                    }
                }
                else if (unitT.IsGod())
                {
                    if (AboutGameC.LessonT == LessonTypes.SettingGod)
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