using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        public void SetMistake(in MistakeTypes mistakeT, in float timer)
        {
            _eMG.MistakeTC.MistakeT = mistakeT;
            _eMG.MistakeTimerC.Timer = timer;
        }
    }
}