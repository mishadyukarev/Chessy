using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    sealed class MistakeS : SystemModelGameAbs
    {
        internal MistakeS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Mistake(in MistakeTypes mistakeT)
        {
            e.MistakeC.MistakeT = mistakeT;
            e.MistakeC.Timer = 0;
            e.Sound(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}