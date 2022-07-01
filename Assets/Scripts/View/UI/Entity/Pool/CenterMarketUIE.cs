using Chessy.Model;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct CenterMarketUIE
    {
        readonly Dictionary<MarketBuyTypes, ButtonUIC> _buttons;
        readonly Dictionary<MarketBuyTypes, TextUIC> _texts1;
        readonly Dictionary<MarketBuyTypes, TextUIC> _texts2;

        public readonly GameObjectVC Zone;
        public readonly ButtonUIC ExitButtonC;
        public ButtonUIC ButtonUIC(in MarketBuyTypes marketBuyT) => _buttons[marketBuyT];
        public TextUIC Text1C(in MarketBuyTypes marketBuyT) => _texts1[marketBuyT];
        public TextUIC Text2C(in MarketBuyTypes marketBuyT) => _texts2[marketBuyT];

        internal CenterMarketUIE(in Transform leftZone)
        {
            _buttons = new Dictionary<MarketBuyTypes, ButtonUIC>();
            _texts1 = new Dictionary<MarketBuyTypes, TextUIC>();
            _texts2 = new Dictionary<MarketBuyTypes, TextUIC>();


            var marketZone = leftZone.Find("Market+");


            Zone = new GameObjectVC(marketZone.gameObject);

            ExitButtonC = new ButtonUIC(marketZone.Find("Exit").Find("Button").GetComponent<Button>());


            for (var marketT = MarketBuyTypes.None + 1; marketT < MarketBuyTypes.End; marketT++)
            {
                var zone = marketZone.Find(marketT.ToString() + "+");

                _buttons.Add(marketT, new ButtonUIC(zone.Find("Button+").GetComponent<Button>()));

                _texts1.Add(marketT, new TextUIC(zone.Find("Text1_TMP+").GetComponent<TextMeshProUGUI>()));
                _texts2.Add(marketT, new TextUIC(zone.Find("Text2_TMP+").GetComponent<TextMeshProUGUI>()));
            }
        }
    }
}