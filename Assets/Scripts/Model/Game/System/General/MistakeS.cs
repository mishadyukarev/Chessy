using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class MistakeS : SystemModel
    {
        internal MistakeS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Mistake(in MistakeTypes mistakeT)
        {
            eMG.MistakeTC.MistakeT = mistakeT;
            eMG.MistakeTimerC.Timer = 0;
            eMG.SoundActionC(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}