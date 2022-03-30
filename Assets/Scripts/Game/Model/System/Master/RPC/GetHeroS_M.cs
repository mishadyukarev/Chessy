using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    sealed class GetHeroS_M : SystemModelGameAbs
    {
        internal GetHeroS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Get(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.LessonTC.LessonT == LessonTypes.PickingGod)
            {
                e.LessonTC.SetNextLesson();
            }

            e.PlayerInfoE(whoseMove).MyHeroTC.Unit = unitT;
            e.PlayerInfoE(whoseMove).HaveHeroInInventor = true;
        }
    }
}