using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class MistakeS : SystemModelGameAbs
    {
        internal MistakeS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Mistake(in MistakeTypes mistakeT)
        {
            eMG.MistakeE.MistakeT = mistakeT;
            eMG.MistakeE.Timer = 0;
            eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}