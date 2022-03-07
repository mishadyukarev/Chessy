//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Chessy.Game
//{
//    public sealed class DownScoutUIE
//    {
//        public readonly ButtonUIC ButtonC;
//        public readonly TextUIC CooldownTextC;

//        public DownScoutUIE(in Transform down)
//        {
//            var button = down.Find(UnitTypes.Scout.ToString() + "_Button").GetComponent<Button>();

//            ButtonC = new ButtonUIC(button);
//            CooldownTextC = new TextUIC(button.transform.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
//        }
//    }
//}