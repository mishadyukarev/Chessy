using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    sealed class TryGivePeopleToEachPlayerS_M : SystemModel
    {
        internal TryGivePeopleToEachPlayerS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {

        }

        internal void TryGive()
        {
            if (eMG.MotionsC.Motions % 5 == 0)
            {
                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                {
                    eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity++;
                }
            }
        }
    }
}