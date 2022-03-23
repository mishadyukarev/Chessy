using Chessy.Common.Component;
using Chessy.Common.View.UI.Component;
using Chessy.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Common.View.UI
{
    public struct SettingsUIE
    {
        public readonly GameObjectVC ParentGOC;

        public readonly ButtonUIC ExitButtonC;
        public readonly SliderUIC SliderC;
        public readonly ToggleUIC HintToggleC;
  

        public SettingsUIE(in Transform settingsZone)
        {
            ParentGOC = new GameObjectVC(settingsZone.gameObject);
            ExitButtonC = new ButtonUIC(settingsZone.Find("ExitCross+").Find("Button+").GetComponent<Button>());
            SliderC = new SliderUIC(settingsZone.Find("Slider+").GetComponent<Slider>());
            HintToggleC = new ToggleUIC(settingsZone.Find("Hint_Toggle+").GetComponent<Toggle>());
        }
    }
}