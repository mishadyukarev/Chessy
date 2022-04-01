using Chessy.Game.Entity.Model;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class MistakeS : SystemModelGameAbs
    {
        internal MistakeS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Mistake(in MistakeTypes mistakeT)
        {
            eMG.MistakeC.MistakeT = mistakeT;
            eMG.MistakeC.Timer = 0;
            eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}