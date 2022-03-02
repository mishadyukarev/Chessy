using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public sealed class DownPawnUIE
    {
        public ButtonUIC ButtonUIC;
        public TextUIC AmountTextC;
        public TextUIC MaxPawnsTextC;

        public DownPawnUIE(in Transform downZone)
        {
            var pawnT = downZone.Find(UnitTypes.Pawn.ToString());

            var button = pawnT.Find("Button").GetComponent<Button>();

            ButtonUIC = new ButtonUIC(button);
            AmountTextC = new TextUIC(pawnT.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            MaxPawnsTextC = new TextUIC(pawnT.Find("MaxPeople_TextMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}
