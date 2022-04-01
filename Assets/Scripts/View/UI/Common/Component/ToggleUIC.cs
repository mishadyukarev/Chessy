using UnityEngine.UI;

namespace Chessy.Common.View.UI.Component
{
    public struct ToggleUIC
    {
        public Toggle Toggle;

        public ToggleUIC(in Toggle toggle) => Toggle = toggle;
    }
}