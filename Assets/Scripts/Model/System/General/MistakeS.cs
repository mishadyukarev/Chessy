namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void Mistake(in MistakeTypes mistakeT)
        {
            _mistakeC.MistakeT = mistakeT;
            _mistakeC.Timer = 0;
            _e.SoundAction(ClipTypes.WritePensil).Invoke();
        }
    }
}