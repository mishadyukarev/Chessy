namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void Mistake(in MistakeTypes mistakeT)
        {
            mistakeC.MistakeT = mistakeT;
            mistakeC.Timer = 0;
            dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();
        }
    }
}