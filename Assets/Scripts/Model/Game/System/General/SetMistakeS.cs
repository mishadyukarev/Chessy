using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetMistakeS : SystemModelGameAbs
    {
        internal SetMistakeS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        public void Set(in MistakeTypes mistakeT, in float timer)
        {
            eMG.MistakeE.MistakeT = mistakeT;
            eMG.MistakeE.Timer = timer;
        }
    }
}