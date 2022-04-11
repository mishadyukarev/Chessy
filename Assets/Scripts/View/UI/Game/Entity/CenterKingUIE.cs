using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CenterKingUIE
    {
        public Chessy.Common.Component.GameObjectVC Paren;
        public ButtonUIC Button;

        public CenterKingUIE(in Transform centerZone)
        {
            var king = centerZone.Find("KingZone");

            Paren = new Chessy.Common.Component.GameObjectVC(king.gameObject);
            Button = new ButtonUIC(king.Find("King+").Find("SetKing_Button").GetComponent<Button>());
        }
    }
}
