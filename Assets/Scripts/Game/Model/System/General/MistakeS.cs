using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class MistakeS : SystemModelGameAbs
    {
        public MistakeS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Mistake(in MistakeTypes mistakeT)
        {
            e.MistakeC.MistakeT = mistakeT;
            e.MistakeC.Timer = 0;
            e.Sound(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}