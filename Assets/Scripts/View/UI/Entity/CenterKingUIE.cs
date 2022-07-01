using Chessy.View.Component;
using Chessy.View.UI.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct CenterKingUIE
    {
        public GameObjectVC Paren;
        public ButtonUIC Button;

        public CenterKingUIE(in Transform centerZone)
        {
            var king = centerZone.Find("KingZone");

            Paren = new GameObjectVC(king.gameObject);
            Button = new ButtonUIC(king.Find("King+").Find("SetKing_Button").GetComponent<Button>());
        }
    }
}
