using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public struct GetHeroS_M
    {
        readonly EntitiesModelGame _eMGame;

        public GetHeroS_M(in EntitiesModelGame eMGame)
        {
            _eMGame = eMGame;
        }
        public void Get(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = _eMGame.WhoseMove.Player;

            if (_eMGame.LessonTC.LessonT == LessonTypes.PickGod)
            {
                _eMGame.LessonTC.SetNextLesson();
            }

            _eMGame.PlayerInfoE(whoseMove).MyHeroTC.Unit = unitT;
            _eMGame.PlayerInfoE(whoseMove).HaveHeroInInventor = true;
        }
    }
}