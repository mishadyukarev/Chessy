namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        public void SetMistake(in MistakeTypes mistakeT, in float timer)
        {
            _e.MistakeT = mistakeT;
            _e.MistakeTimer = timer;
        }
    }
}