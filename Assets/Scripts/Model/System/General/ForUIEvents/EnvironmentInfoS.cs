namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void EnvironmentClick()
        {
            dataFromViewC.SoundAction(ClipTypes.Click).Invoke();
            zonesInfoC.IsActiveEnvironment = !zonesInfoC.IsActiveEnvironment;

            updateAllViewC.NeedUpdateView = true;
        }
    }
}