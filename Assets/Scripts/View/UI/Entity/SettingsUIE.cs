using Chessy.View.Component;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct SettingsUIE
    {
        internal readonly GameObjectVC ParentGOC;

        internal readonly ButtonUIC ExitButtonC;
        internal readonly SliderUIC SliderC;
        internal readonly ToggleUIC HintToggleC;
        internal readonly TMP_Dropdown Dropdown;


        internal SettingsUIE(in Transform settingsZone)
        {
            ParentGOC = new GameObjectVC(settingsZone.gameObject);
            ExitButtonC = new ButtonUIC(settingsZone.Find("ExitCross+").Find("Button+").GetComponent<Button>());
            SliderC = new SliderUIC(settingsZone.Find("Slider+").GetComponent<Slider>());
            HintToggleC = new ToggleUIC(settingsZone.Find("Hint_Toggle+").GetComponent<Toggle>());
            Dropdown = settingsZone.Find("Dropdown+").GetComponent<TMP_Dropdown>();
        }
    }
}