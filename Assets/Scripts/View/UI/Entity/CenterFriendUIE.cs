using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model
{
    public struct CenterFriendUIE
    {
        public TextUIC TextC;
        public ButtonUIC ButtonC;

        public CenterFriendUIE(in Transform centerZone)
        {
            var friendZone = centerZone.Find("FriendZone");

            TextC = new TextUIC(friendZone.Find("WhoseMotion_TextMP").GetComponent<TextMeshProUGUI>());
            ButtonC = new ButtonUIC(friendZone.Find("Ready_Button").GetComponent<Button>());
        }
    }
}