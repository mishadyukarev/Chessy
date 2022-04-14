using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetMistakeS : SystemModel
    {
        internal SetMistakeS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void Set(in MistakeTypes mistakeT, in float timer)
        {
            eMG.MistakeTC.MistakeT = mistakeT;
            eMG.MistakeTimerC.Timer = timer;
        }
    }
}