namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void ClickSkipLesson()
        {
            _s.StartGame(false);
        }
    }
}