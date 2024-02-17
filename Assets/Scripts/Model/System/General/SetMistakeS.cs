namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        public void SetMistake(in MistakeTypes mistakeT, in float timer)
        {
            mistakeC.MistakeT = mistakeT;
            mistakeC.Timer = timer;
        }
    }
}