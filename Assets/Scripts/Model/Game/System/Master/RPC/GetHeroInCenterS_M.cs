using Chessy.Game.Enum;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame
    {
        internal void GetHeroInCenterM(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (_eMG.LessonT == LessonTypes.PickingGod)
            {
                _eMG.LessonTC.SetNextLesson();
            }

            _eMG.PlayerInfoE(whoseMove).GodInfoE.UnitT = unitT;
            _eMG.PlayerInfoE(whoseMove).GodInfoE.HaveHeroInInventor = true;
        }
    }
}