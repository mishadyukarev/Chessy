namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void EnvironmentClick()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _e.ZoneInfoC.IsActiveEnvironment = !_e.ZoneInfoC.IsActiveEnvironment;

            _e.NeedUpdateView = true;
        }
    }
}