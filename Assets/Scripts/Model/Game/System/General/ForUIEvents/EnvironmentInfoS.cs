using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void EnvironmentClick()
        {
            _e.Com.SoundActionC(ClipCommonTypes.Click).Invoke();
            _e.ZoneInfoC.IsActiveEnvironment = !_e.ZoneInfoC.IsActiveEnvironment;

            _e.NeedUpdateView = true;
        }
    }
}