namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void ClickSkipLesson()
        {
            _s.StartGame(false);
        }
    }
}