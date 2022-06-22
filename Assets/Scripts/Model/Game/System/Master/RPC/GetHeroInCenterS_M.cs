using Chessy.Game.Enum;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void GetHeroInCenterM(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            if (_e.LessonT == LessonTypes.PickingGod)
            {
                _e.LessonT.SetNextLesson();
            }

            _e.PlayerInfoE(whoseMove).GodInfoE.UnitT = unitT;
            _e.PlayerInfoE(whoseMove).GodInfoE.HaveHeroInInventor = true;
        }
    }
}