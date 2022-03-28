using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class MistakeS : SystemModelGameAbs
    {
        public MistakeS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Mistake(in MistakeTypes mistakeT)
        {
            eMGame.MistakeC.MistakeT = mistakeT;
            eMGame.MistakeC.Timer = 0;
            eMGame.Sound(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}