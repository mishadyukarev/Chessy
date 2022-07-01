using UnityEngine.UI;
namespace Chessy.View.UI.Component
{
    public struct ToggleUIC
    {
        public Toggle Toggle;

        public ToggleUIC(in Toggle toggle) => Toggle = toggle;
    }
}