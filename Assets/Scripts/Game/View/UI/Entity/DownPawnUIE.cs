using Chessy.Common.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct DownPawnUIE
    {
        public readonly GameObjectVC ParenGOC;
        public readonly ButtonUIC ButtonUIC;
        public readonly TextUIC AmountTextC;
        public readonly TextUIC MaxPawnsTextC;

        public DownPawnUIE(in Transform downZone)
        {
            var pawnT = downZone.Find(UnitTypes.Pawn.ToString());

            ParenGOC = new GameObjectVC(pawnT.gameObject);

            var button = pawnT.Find("Button").GetComponent<Button>();

            ButtonUIC = new ButtonUIC(button);
            AmountTextC = new TextUIC(pawnT.Find("Text (TMP)").GetComponent<TextMeshProUGUI>());

            MaxPawnsTextC = new TextUIC(pawnT.Find("MaxPeople_TextMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}
