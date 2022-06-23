namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        public void SetMistake(in MistakeTypes mistakeT, in float timer)
        {
            _e.MistakeT = mistakeT;
            _e.MistakeTimer = timer;
        }
    }
}