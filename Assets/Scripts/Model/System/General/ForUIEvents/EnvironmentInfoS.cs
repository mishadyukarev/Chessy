namespace Chessy.Model
{
    public sealed partial class SystemsModelGameForUI
    {
        public void EnvironmentClick()
        {
            _e.SoundAction(ClipTypes.Click).Invoke();
            _e.ZoneInfoC.IsActiveEnvironment = !_e.ZoneInfoC.IsActiveEnvironment;

            _e.NeedUpdateView = true;
        }
    }
}