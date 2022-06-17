using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void EnvironmentClick()
        {
            _eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();
            _eMG.ZoneInfoC.IsActiveEnvironment = !_eMG.ZoneInfoC.IsActiveEnvironment;

            _eMG.NeedUpdateView = true;
        }
    }
}