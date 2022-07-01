using Chessy.Model;
using Chessy.Model.Component;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct DownPawnUIE
    {
        public readonly GameObjectVC ParenGOC;
        public readonly AnimationVC AnimationC;
        public readonly ButtonUIC ButtonC;
        public readonly TextUIC AmountTextC;
        public readonly TextUIC MaxPawnsTextC;

        public DownPawnUIE(in Transform downZone)
        {
            var pawnT = downZone.Find(UnitTypes.Pawn.ToString() + "+");

            ParenGOC = new GameObjectVC(pawnT.gameObject);
            AnimationC = new AnimationVC(pawnT.Find("Image+").GetComponent<Animation>());

            var button = pawnT.Find("Button+").GetComponent<Button>();

            ButtonC = new ButtonUIC(button);
            AmountTextC = new TextUIC(pawnT.Find("Text (TMP)+").GetComponent<TextMeshProUGUI>());

            MaxPawnsTextC = new TextUIC(pawnT.Find("MaxPeople_TextMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}
