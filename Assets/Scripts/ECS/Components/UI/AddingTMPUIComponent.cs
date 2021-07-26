using TMPro;

namespace Assets.Scripts.ECS.Components.UI
{
    internal struct AddingTMPUIComponent
    {
        internal TextMeshProUGUI TextMeshProUGUI { get; private set; }

        internal AddingTMPUIComponent(TextMeshProUGUI text) => TextMeshProUGUI = text;
    }
}
