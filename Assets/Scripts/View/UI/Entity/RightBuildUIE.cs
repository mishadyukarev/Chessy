﻿using Chessy.View.Component;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct RightBuildUIE
    {
        public GameObjectVC Parent;
        public ButtonUIC Button;
        public TextUIC Text;

        public RightBuildUIE(in Transform button)
        {
            Parent = new GameObjectVC(button.gameObject);
            Button = new ButtonUIC(button.Find("Button").GetComponent<Button>());
            Text = new TextUIC(button.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
        }
    }
}
