using Chessy.Model.Enum;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void GetHeroInCenterM(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            if (_e.LessonT == LessonTypes.PickingGod)
            {
                 SetNextLesson();
            }

            _e.PlayerInfoE(whoseMove).GodInfoC.UnitT = unitT;
            _e.PlayerInfoE(whoseMove).GodInfoC.HaveGodInInventor = true;
        }
    }
}