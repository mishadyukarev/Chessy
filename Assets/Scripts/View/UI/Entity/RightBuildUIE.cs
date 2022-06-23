using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model
{
    public struct RightBuildUIE
    {
        public Chessy.Common.Component.GameObjectVC Parent;
        public ButtonUIC Button;
        public TextUIC Text;

        public RightBuildUIE(in Transform button)
        {
            Parent = new Chessy.Common.Component.GameObjectVC(button.gameObject);
            Button = new ButtonUIC(button.Find("Button").GetComponent<Button>());
            Text = new TextUIC(button.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }
    }
}
