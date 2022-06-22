namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void Mistake(in MistakeTypes mistakeT)
        {
            _e.MistakeT = mistakeT;
            _e.MistakeTimer = 0;
            _e.SoundAction(ClipTypes.WritePensil).Invoke();
        }
    }
}