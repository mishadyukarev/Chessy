namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void ClickSkipLesson()
        {
            _s.StartGame(false);
        }
    }
}