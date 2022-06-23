using Chessy.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct CenterUIE
    {
        public readonly TextUIC LogTextC;
        public readonly ButtonUIC DiscordButtonC;
        public readonly ButtonUIC BookButtonC;
        public readonly ButtonUIC SettingsButtonC;
        public readonly ButtonUIC LikeGameButtonC;
        public readonly ButtonUIC ExitButtonC;

        public CenterUIE(in Transform centerZone)
        {
            LogTextC = new TextUIC(centerZone.Find("Log_TextMP").GetComponent<TextMeshProUGUI>());

            DiscordButtonC = new ButtonUIC(centerZone.Find("JoinDiscord+").Find("Button+").GetComponent<Button>());

            LikeGameButtonC = new ButtonUIC(centerZone.Find("LikeGame+").Find("Button+").GetComponent<Button>());
            ExitButtonC = new ButtonUIC(centerZone.Find("Exit+").Find("Button+").GetComponent<Button>());

            BookButtonC = new ButtonUIC(centerZone.Find("BookGuid+").Find("Button+").GetComponent<Button>());
            SettingsButtonC = new ButtonUIC(centerZone.Find("Settings+").Find("Button+").GetComponent<Button>());
        }
    }
}
