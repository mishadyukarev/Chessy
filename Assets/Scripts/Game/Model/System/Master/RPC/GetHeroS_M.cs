using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public sealed class GetHeroS_M : SystemModelGameAbs
    {
        public GetHeroS_M(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Get(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = eMGame.WhoseMove.Player;

            if (eMGame.LessonTC.LessonT == LessonTypes.PickGod)
            {
                eMGame.LessonTC.SetNextLesson();
            }

            eMGame.PlayerInfoE(whoseMove).MyHeroTC.Unit = unitT;
            eMGame.PlayerInfoE(whoseMove).HaveHeroInInventor = true;
        }
    }
}