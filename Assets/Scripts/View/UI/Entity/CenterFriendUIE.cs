using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct CenterFriendUIE
    {
        public readonly TextUIC TextC;
        public readonly ButtonUIC ButtonC;

        public CenterFriendUIE(in Transform centerZone)
        {
            var friendZone = centerZone.Find("FriendZone");

            TextC = new TextUIC(friendZone.Find("WhoseMotion_TextMP").GetComponent<TextMeshProUGUI>());
            ButtonC = new ButtonUIC(friendZone.Find("Ready_Button").GetComponent<Button>());
        }
    }
}