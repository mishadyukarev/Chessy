namespace Chessy.Model
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