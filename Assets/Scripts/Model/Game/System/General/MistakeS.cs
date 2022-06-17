using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void Mistake(in MistakeTypes mistakeT)
        {
            _eMG.MistakeTC.MistakeT = mistakeT;
            _eMG.MistakeTimerC.Timer = 0;
            _eMG.SoundAction(ClipTypes.WritePensil).Invoke();
        }
    }
}