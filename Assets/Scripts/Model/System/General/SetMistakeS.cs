namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        public void SetMistake(in MistakeTypes mistakeT, in float timer)
        {
            _mistakeC.MistakeT = mistakeT;
            _mistakeC.Timer = timer;
        }
    }
}