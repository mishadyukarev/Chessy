using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct DownPawnUIE
    {
        public ButtonUIC ButtonUIC;
        public TextUIC TextUIC;

        public TextUIC MaxPawns;

        public DownPawnUIE(in Transform downZone)
        {
            var pawnT = downZone.Find(UnitTypes.Pawn.ToString());

            var button = pawnT.Find("Button").GetComponent<Button>();

            ButtonUIC = new ButtonUIC(button);
            TextUIC = new TextUIC(pawnT.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            MaxPawns = new TextUIC(pawnT.Find("MaxPeople_TextMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}
