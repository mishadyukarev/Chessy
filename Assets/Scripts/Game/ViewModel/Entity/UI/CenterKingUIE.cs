using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct CenterKingUIE
    {
        public GameObjectVC Paren;
        public ButtonUIC Button;

        public CenterKingUIE(in Transform centerZone)
        {
            var king = centerZone.Find("KingZone");

            Paren = new GameObjectVC(king.gameObject);
            Button = new ButtonUIC(king.Find("SetKing_Button").GetComponent<Button>());
        }
    }
}
