using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class GetHeroS_M : SystemModelGameAbs
    {
        internal GetHeroS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in UnitTypes unitT, in Player sender)
        {
            var whoseMove = eMG.WhoseMove.PlayerT;

            if (eMG.LessonTC.LessonT == LessonTypes.PickingGod)
            {
                eMG.LessonTC.SetNextLesson();
            }

            eMG.PlayerInfoE(whoseMove).MyHeroTC.UnitT = unitT;
            eMG.PlayerInfoE(whoseMove).HaveHeroInInventor = true;
        }
    }
}