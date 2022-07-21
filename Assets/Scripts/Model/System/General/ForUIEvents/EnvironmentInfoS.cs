namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void EnvironmentClick()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _zonesInfoC.IsActiveEnvironment = !_zonesInfoC.IsActiveEnvironment;

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}