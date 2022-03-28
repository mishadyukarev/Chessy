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
            var whoseMove = e.WhoseMove.Player;

            if (e.LessonTC.LessonT == LessonTypes.PickGod)
            {
                e.LessonTC.SetNextLesson();
            }

            e.PlayerInfoE(whoseMove).MyHeroTC.Unit = unitT;
            e.PlayerInfoE(whoseMove).HaveHeroInInventor = true;
        }
    }
}